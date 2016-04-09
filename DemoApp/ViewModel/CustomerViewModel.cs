using System;
using System.ComponentModel;
using System.Windows.Input;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// Customer 개체를 위한 래퍼
    /// (A UI-friendly wrapper for a Customer object.)
    /// </summary>
    public class CustomerViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        #region Fields

        readonly Customer _customer;
        readonly CustomerRepository _customerRepository;
        string _customerType;
        string[] _customerTypeOptions;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

        public CustomerViewModel( Customer customer, CustomerRepository customerRepository )
        {
            if( customer == null )
                throw new ArgumentNullException( nameof(customer) );

            if( customerRepository == null )
                throw new ArgumentNullException( nameof(customerRepository) );

            _customer = customer;
            _customerRepository = customerRepository;
            _customerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
        }

        #endregion // Constructor

        #region Customer Properties

        public string Email
        {
            get { return _customer.Email; }
            set
            {
                if( value == _customer.Email )
                    return;

                _customer.Email = value;

                base.OnPropertyChanged( nameof( Email ) );
            }
        }

        public string FirstName
        {
            get { return _customer.FirstName; }
            set
            {
                if( value == _customer.FirstName )
                    return;

                _customer.FirstName = value;

                base.OnPropertyChanged( nameof( FirstName ) );
            }
        }

        public bool IsCompany
        {
            get { return _customer.IsCompany; }
        }

        public string LastName
        {
            get { return _customer.LastName; }
            set
            {
                if( value == _customer.LastName )
                    return;

                _customer.LastName = value;

                base.OnPropertyChanged( nameof( LastName ) );
            }
        }

        public double TotalSales
        {
            get { return _customer.TotalSales; }
        }

        #endregion // Customer Properties

        #region Presentation Properties

        /// <summary>
        /// Customer의 유형을 나타내는 값을 가져 오거나 설정합니다.
        /// 이 속성은 고객 클래스의 IsCompany속성에 매핑하고, 또한 'unselected' 상태를 위한 지원도 합니다.
        /// </summary>
        public string CustomerType
        {
            get { return _customerType; }
            set
            {
                if( value == _customerType || String.IsNullOrEmpty( value ) )
                    return;

                _customerType = value;

                if( _customerType == Strings.CustomerViewModel_CustomerTypeOption_Company )
                {
                    _customer.IsCompany = true;
                }
                else if( _customerType == Strings.CustomerViewModel_CustomerTypeOption_Person )
                {
                    _customer.IsCompany = false;
                }

                base.OnPropertyChanged( nameof( CustomerType ) );

                // customer개체들의 IsCompany 속성 유형(즉, 텍스트)을 해석하는 방법을 
                // 이 ViewModel 개체가 가지고 있다.
                // LastName 속성은 Customer의 회사 여부에 따라 다르게 확인되므로 
                // LastName 속성에 대한 검증을 반드시 실행해야합니다
                base.OnPropertyChanged( nameof( LastName ) );
            }
        }

        /// <summary>
        /// 고객 유형 선택자에 문자열 목록을 채워 반환합니다.
        /// (Returns a list of strings used to populate the Customer Type selector.)
        /// </summary>
        public string[] CustomerTypeOptions
        {
            get
            {
                if( _customerTypeOptions == null )
                {
                    _customerTypeOptions = new string[]
                    {
                        Strings.CustomerViewModel_CustomerTypeOption_NotSpecified,
                        Strings.CustomerViewModel_CustomerTypeOption_Person,
                        Strings.CustomerViewModel_CustomerTypeOption_Company
                    };
                }
                return _customerTypeOptions;
            }
        }

        public override string DisplayName
        {
            get
            {
                if( this.IsNewCustomer )
                {
                    return Strings.CustomerViewModel_DisplayName;
                }
                else if( _customer.IsCompany )
                {
                    return _customer.FirstName;
                }
                else
                {
                    return String.Format( "{0}, {1}", _customer.LastName, _customer.FirstName );
                }
            }
        }

        /// <summary>
        /// UI에서 선택된 customer를 설정하거나 반환합니다.
        /// (Gets/sets whether this customer is selected in the UI.)
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if( value == _isSelected )
                    return;

                _isSelected = value;

                base.OnPropertyChanged( nameof(IsSelected) );
            }
        }

        /// <summary>
        /// 고객을 저장하는 command를 반환합니다.
        /// (Returns a command that saves the customer.)
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if( _saveCommand == null )
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// repository에 고객을 저장합니다. SaveCommand 메서드에 의해 호출됩니다.
        /// (Saves the customer to the repository.  This method is invoked by the SaveCommand.)
        /// </summary>
        public void Save()
        {
            if( !_customer.IsValid )
                throw new InvalidOperationException( Strings.CustomerViewModel_Exception_CannotSave );

            if( this.IsNewCustomer )
                _customerRepository.AddCustomer( _customer );

            base.OnPropertyChanged( "DisplayName" );
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// cuntomer가 사용자에 의해 생성되고 저장소에 저장되지 않았다면 true를 반환합니다.
        /// </summary>
        bool IsNewCustomer
        {
            get { return !_customerRepository.ContainsCustomer( _customer ); }
        }

        /// <summary>
        /// customer가 유효하고 저장할 수 있는 경우에 true 반환합니다.
        /// (Returns true if the customer is valid and can be saved.)
        /// </summary>
        bool CanSave
        {
            get { return String.IsNullOrEmpty( this.ValidateCustomerType() ) && _customer.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return ( _customer as IDataErrorInfo ).Error; }
        }

        string IDataErrorInfo.this[ string propertyName ]
        {
            get
            {
                string error = null;

                if( propertyName == "CustomerType" )
                {
                    // Customer클래스의 IsCompany속성은 부울(Boolean)이고 
                    // '선택되지않은' 상태에 대한 부울( Boolean )은 제공하지 않는다.
                    // CustomerViewModel 클래스는 매핑 및 유효성 검사를 처리합니다.
                    error = this.ValidateCustomerType();
                }
                else
                {
                    error = ( _customer as IDataErrorInfo )[ propertyName ];
                }

                // CommandManger는 여러 commands를 등록할 수 있습니다.
                // Save 명령 실행을 지금 할 수 있는지 쿼리합니다. 
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }

        string ValidateCustomerType()
        {
            if( this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
               this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person )
                return null;

            return Strings.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion // IDataErrorInfo Members
    }
}