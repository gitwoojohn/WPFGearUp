﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoApp.Properties {
    using System;
    
    
    /// <summary>
    ///   지역화된 문자열 등을 찾기 위한 강력한 형식의 리소스 클래스입니다.
    /// </summary>
    // 이 클래스는 ResGen 또는 Visual Studio와 같은 도구를 통해 StronglyTypedResourceBuilder
    // 클래스에서 자동으로 생성되었습니다.
    // 멤버를 추가하거나 제거하려면 .ResX 파일을 편집한 다음 /str 옵션을 사용하여 ResGen을
    // 다시 실행하거나 VS 프로젝트를 다시 빌드하십시오.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   이 클래스에서 사용하는 캐시된 ResourceManager 인스턴스를 반환합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DemoApp.Properties.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   이 강력한 형식의 리소스 클래스를 사용하여 모든 리소스 조회에 대한 현재 스레드의 CurrentUICulture
        ///   속성을 재정의합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   All Customers과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string AllCustomersViewModel_DisplayName {
            get {
                return ResourceManager.GetString("AllCustomersViewModel_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Companies have no last name과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Customer_Error_CompanyHasNoLastName {
            get {
                return ResourceManager.GetString("Customer_Error_CompanyHasNoLastName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   E-mail address is invalid과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Customer_Error_InvalidEmail {
            get {
                return ResourceManager.GetString("Customer_Error_InvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   E-mail address is missing과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Customer_Error_MissingEmail {
            get {
                return ResourceManager.GetString("Customer_Error_MissingEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   First name is missing과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Customer_Error_MissingFirstName {
            get {
                return ResourceManager.GetString("Customer_Error_MissingFirstName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Last name is missing과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Customer_Error_MissingLastName {
            get {
                return ResourceManager.GetString("Customer_Error_MissingLastName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Company과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_Company {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_Company", resourceCulture);
            }
        }
        
        /// <summary>
        ///   (Not Specified)과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_NotSpecified {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_NotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Person과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_Person {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_Person", resourceCulture);
            }
        }
        
        /// <summary>
        ///   New Customer과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_DisplayName {
            get {
                return ResourceManager.GetString("CustomerViewModel_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Customer type must be selected과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_Error_MissingCustomerType {
            get {
                return ResourceManager.GetString("CustomerViewModel_Error_MissingCustomerType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cannot save an invalid customer.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CustomerViewModel_Exception_CannotSave {
            get {
                return ResourceManager.GetString("CustomerViewModel_Exception_CannotSave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Create new customer과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string MainWindowViewModel_Command_CreateNewCustomer {
            get {
                return ResourceManager.GetString("MainWindowViewModel_Command_CreateNewCustomer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   View all customers과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string MainWindowViewModel_Command_ViewAllCustomers {
            get {
                return ResourceManager.GetString("MainWindowViewModel_Command_ViewAllCustomers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   MVVM Demo App과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string MainWindowViewModel_DisplayName {
            get {
                return ResourceManager.GetString("MainWindowViewModel_DisplayName", resourceCulture);
            }
        }
    }
}
