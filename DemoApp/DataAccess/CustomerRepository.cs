using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using DemoApp.Model;

namespace DemoApp.DataAccess
{
    /// <summary>
    /// 응용 프로그램에서 고객의 소스를 나타냅니다.
    /// </summary>
    public class CustomerRepository
    {
        #region Fields

        readonly List<Customer> _customers;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// 고객의 새로운 저장소 생성
        /// </summary>
        /// <param name="customerDataFile">고객 데이터를 포함하는 XML 리소스 파일에 대한 상대 경로.</param>
        public CustomerRepository( string customerDataFile )
        {
            _customers = LoadCustomers( customerDataFile );
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// 고객이 저장소에 배치 될 때 발생합니다.
        /// </summary>
        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;

        /// <summary>
        /// 저장소에 지정된 고객을 배치합니다.
        /// 고객이 저장소에있는 경우에는 예외가 발생하지 않습니다.
        /// </summary>
        public void AddCustomer( Customer customer )
        {
            if( customer == null )
                throw new ArgumentNullException( "customer" );

            if( !_customers.Contains( customer ) )
            {
                _customers.Add( customer );

                CustomerAdded?.Invoke( this, new CustomerAddedEventArgs( customer ) );
            }
        }

        /// <summary>
        /// 지정된 고객이 저장소에 존재하거나 null이 아닌 경우 true 반환합니다.
        /// </summary>
        public bool ContainsCustomer( Customer customer )
        {
            if( customer == null )
                throw new ArgumentNullException( nameof( customer ) );

            return _customers.Contains( customer );
        }

        /// <summary>
        /// 저장소에 있는 모든 고객들의 목록을 shallow-copied 합니다.
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        public List<Customer> GetCustomers()
        {
            return new List<Customer>( _customers );
        }

        #endregion // Public Interface

        #region Private Helpers

        static List<Customer> LoadCustomers( string customerDataFile )
        {
            // In a real application, the data would come from an external source,
            // but for this demo let's keep things simple and use a resource file.
            using( Stream stream = GetResourceStream( customerDataFile ) )
            using( XmlReader xmlRdr = new XmlTextReader( stream ) )
                return
                    ( from customerElem in XDocument.Load( xmlRdr ).Element( "customers" ).Elements( "customer" )
                      select Customer.CreateCustomer(
                         ( double )customerElem.Attribute( "totalSales" ),
                         ( string )customerElem.Attribute( "firstName" ),
                         ( string )customerElem.Attribute( "lastName" ),
                         ( bool )customerElem.Attribute( "isCompany" ),
                         ( string )customerElem.Attribute( "email" )
                          ) ).ToList();
        }

        static Stream GetResourceStream( string resourceFile )
        {
            Uri uri = new Uri( resourceFile, UriKind.RelativeOrAbsolute );

            StreamResourceInfo info = Application.GetResourceStream( uri );
            if( info == null || info.Stream == null )
                throw new ApplicationException( "Missing resource file: " + resourceFile );

            return info.Stream;
        }

        #endregion // Private Helpers
    }
}
