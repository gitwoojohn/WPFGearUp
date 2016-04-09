using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DemoApp.Properties;

namespace DemoApp.Model
{
    /// <summary>
    /// 회사의 고객을 나타냅니다. 이 클래스는 내장된 유효성 검사 논리입니다. 
    /// <para>
    /// 간단하게 표시되는 WPF 사용자 인터페이스에 의해 편집 될 수 있도록
    /// CustomerViewModel 클래스에 의해 캡슐화 되어있습니다.
    /// </para>
    /// </summary>
    public class Customer : IDataErrorInfo
    {
        #region Creation

        public static Customer CreateNewCustomer()
        {
            return new Customer();
        }

        public static Customer CreateCustomer(
            double totalSales,
            string firstName,
            string lastName,
            bool isCompany,
            string email )
        {
            return new Customer
            {
                TotalSales = totalSales,
                FirstName = firstName,
                LastName = lastName,
                IsCompany = isCompany,
                Email = email
            };
        }

        protected Customer()
        {
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the e-mail address for the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 고객의 성을 설정하거나 가져 옵니다.
        /// 이 고객이 회사면 회사의 이름을 저장합니다.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 고객의 company 또는 person 설정하거나 가져옵니다.
        /// 기본값은 false 입니다.
        /// </summary>
        public bool IsCompany { get; set; }

        /// <summary>
        /// 고객의 이름을 저장 하거나 가져 옵니다.
        /// 고객이 회사라면 설정 하지 않습니다.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 고객이 소비한 돈의 총액을 반환 합니다.
        /// (Returns the total amount of money spent by the customer.)
        /// </summary>
        public double TotalSales { get; private set; }

        #endregion // State Properties

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[ string propertyName ]
        {
            get { return this.GetValidationError( propertyName ); }
        }

        #endregion // IDataErrorInfo Members

        #region Validation

        /// <summary>
        /// 이 개체가 유효성 검사 오류가 없는 true를 돌려줍니다.
        /// (Returns true if this object has no validation errors.)
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach( string property in ValidatedProperties )
                    if( GetValidationError( property ) != null )
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties =
        {
            "Email",
            "FirstName",
            "LastName",
        };

        string GetValidationError( string propertyName )
        {
            if( Array.IndexOf( ValidatedProperties, propertyName ) < 0 )
                return null;

            string error = null;

            switch( propertyName )
            {
                case "Email":
                    error = this.ValidateEmail();
                    break;

                case "FirstName":
                    error = this.ValidateFirstName();
                    break;

                case "LastName":
                    error = this.ValidateLastName();
                    break;

                default:
                    Debug.Fail( "Unexpected property being validated on Customer: " + propertyName );
                    break;
            }

            return error;
        }

        string ValidateEmail()
        {
            if( IsStringMissing( this.Email ) )
            {
                return Strings.Customer_Error_MissingEmail;
            }
            else if( !IsValidEmailAddress( this.Email ) )
            {
                return Strings.Customer_Error_InvalidEmail;
            }
            return null;
        }

        string ValidateFirstName()
        {
            if( IsStringMissing( this.FirstName ) )
            {
                return Strings.Customer_Error_MissingFirstName;
            }
            return null;
        }

        string ValidateLastName()
        {
            if( this.IsCompany )
            {
                if( !IsStringMissing( this.LastName ) )
                    return Strings.Customer_Error_CompanyHasNoLastName;
            }
            else
            {
                if( IsStringMissing( this.LastName ) )
                    return Strings.Customer_Error_MissingLastName;
            }
            return null;
        }

        static bool IsStringMissing( string value )
        {
            return
                String.IsNullOrEmpty( value ) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress( string email )
        {
            if( IsStringMissing( email ) )
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch( email, pattern, RegexOptions.IgnoreCase );
        }

        #endregion // Validation
    }
}