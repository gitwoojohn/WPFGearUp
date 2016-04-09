using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// 속성값 변경시 알림과 리소스 해제를 위한 가비지 컬렉션 구현. 
    /// <para>
    /// <see cref="WorkspaceViewModel"/> 클래스에서 이 ViewModelBase를 상속함.
    /// </para>
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Constructor

        protected ViewModelBase() { }

        #endregion // Constructor

        #region DisplayName

        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region Debugging Aides

        /// <summary>
        /// 개발자에게 public 속성을 가지고 있지 않은 이름을 나열합니다.
        /// 릴리스 모드에서는 존재하지 않습니다.
        /// </summary>
        /// <param name="propertyName"></param>
        [Conditional( "DEBUG" )]
        [DebuggerStepThrough]
        public void VerifyPropertyName( string propertyName )
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if( TypeDescriptor.GetProperties( this )[ propertyName ] == null )
            {
                string msg = "Invalid property name: " + propertyName;

                if( ThrowOnInvalidPropertyName )
                    throw new Exception( msg );
                else
                    Debug.Fail( msg );
            }
        }
        public bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// 이 개체의 속성이 새 값을 가질때 발생합니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 이 개체에서 PropertyChanged 이벤트를 발생합니다.
        /// </summary>
        /// <param name="propertyName">이 속성은 새로운 값을 가집니다.</param>
        protected virtual void OnPropertyChanged( string propertyName )
        {
            VerifyPropertyName( propertyName );

            // C# 6 New Feature PropertyChanged이 null이 아니면 이벤트 호출.
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(  propertyName ) );
        }

        #endregion INotifyPropertyChanged Members

        #region IDisposable Members

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// 뷰 모델 개체를 보장하기위해서 필요한 가비지 컬렉션을 합니다.
        /// (Useful for ensuring that ViewModel objects are properly garbage collected.)
        /// </summary>
        ~ViewModelBase()
        {
            string msg = $"{GetType().Name} ({DisplayName}) ({GetHashCode()}) Finalized";
            Debug.WriteLine( msg );
        }

#endif
        #endregion // IDisposable Members
    }
}
