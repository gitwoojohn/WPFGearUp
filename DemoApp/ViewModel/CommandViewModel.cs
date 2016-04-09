using System;
using System.Windows.Input;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// 추상화 클래스 입니다.
    /// <para>
    /// UI에서 Closecommand(File - Exit)를 실행할 때 ViewModelBase의 하위 클래스를 삭제 할 수 있습니다.
    /// </para>
    /// </summary>
    public abstract class CommandViewModel : ViewModelBase
    {
        #region Fields

        RelayCommand _closeCommand;

        #endregion // Fields

        #region Constructor

        protected CommandViewModel() { }

        #endregion // Constructor

        #region CloseCommand

        public ICommand CloseCommand
        {
            get
            {
                if( _closeCommand == null )
                    _closeCommand = new RelayCommand( param => OnRequestClose() );

                return _closeCommand;
            }
        }

        #endregion // CloseCommand

        #region RequestClose [event]

        /// <summary>
        /// UI에서 이 작업공간(workspace)을 삭제해야 할 때 발생합니다.
        /// </summary>
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            RequestClose?.Invoke( this, EventArgs.Empty );
        }

        #endregion // RequestClose [event]
    }
}
