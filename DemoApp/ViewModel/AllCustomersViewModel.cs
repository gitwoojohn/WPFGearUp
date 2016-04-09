using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// CustomerViewModel은 CustomerRepository의 동기화를 제공하는 개체 컨테이너를 나타냅니다.
    /// 이 클래스는 선택한 여러 고객에 관한 정보를 제공합니다.
    /// </summary>
    public class AllCustomersViewModel : WorkspaceViewModel
    {
        #region Fields

        readonly CustomerRepository _customerRepository;

        #endregion // Fields

        #region Constructor

        public AllCustomersViewModel( CustomerRepository customerRepository )
        {
            if( customerRepository == null )
                throw new ArgumentNullException( nameof( customerRepository ) );

            base.DisplayName = Strings.AllCustomersViewModel_DisplayName;

            _customerRepository = customerRepository;

            // Subscribe for notifications of when a new customer is saved.
            _customerRepository.CustomerAdded += this.OnCustomerAddedToRepository;

            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllCustomers();
        }

        void CreateAllCustomers()
        {
            List<CustomerViewModel> all =
                ( from cust in _customerRepository.GetCustomers()
                  select new CustomerViewModel( cust, _customerRepository ) ).ToList();

            foreach( CustomerViewModel cvm in all )
                cvm.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            this.AllCustomers = new ObservableCollection<CustomerViewModel>( all );
            this.AllCustomers.CollectionChanged += this.OnCollectionChanged;
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// CustomerViewModel 개체의 모든 컬렉션을 반환 합니다.
        /// (Returns a collection of all the CustomerViewModel objects.)
        /// </summary>
        public ObservableCollection<CustomerViewModel> AllCustomers { get; private set; }

        /// <summary>
        /// customers의 선택에 대한 모든 판매 합계를 반환합니다.
        /// (Returns the total sales sum of all selected customers.)
        /// </summary>
        public double TotalSelectedSales
        {
            get
            {
                return AllCustomers.Sum(
                    custVM => custVM.IsSelected ? custVM.TotalSales : 0.0 );
            }
        }

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            foreach( CustomerViewModel custVM in AllCustomers )
                custVM.Dispose();

            this.AllCustomers.Clear();
            this.AllCustomers.CollectionChanged -= this.OnCollectionChanged;

            _customerRepository.CustomerAdded -= this.OnCustomerAddedToRepository;
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        void OnCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( e.NewItems != null && e.NewItems.Count != 0 )
                foreach( CustomerViewModel custVM in e.NewItems )
                    custVM.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            if( e.OldItems != null && e.OldItems.Count != 0 )
                foreach( CustomerViewModel custVM in e.OldItems )
                    custVM.PropertyChanged -= this.OnCustomerViewModelPropertyChanged;
        }

        void OnCustomerViewModelPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            string IsSelected = "IsSelected";

            // 참조하는 속성 이름이 유효한지 확인합니다.
            // 이 디버깅 기술은 릴리스 빌드에는 실행되지 않습니다.
            ( sender as CustomerViewModel ).VerifyPropertyName( IsSelected );

            // customer가 선택되거나 선택되지 않을때 새로운 값을 다시 조회 할 수 있도록 
            // TotalSelectedSales 속성 변경을 알려야 합니다.
            if( e.PropertyName == IsSelected )
                OnPropertyChanged( nameof(TotalSelectedSales) );
        }

        void OnCustomerAddedToRepository( object sender, CustomerAddedEventArgs e )
        {
            var viewModel = new CustomerViewModel( e.NewCustomer, _customerRepository );
            this.AllCustomers.Add( viewModel );
        }

        #endregion // Event Handling Methods
    }
}
