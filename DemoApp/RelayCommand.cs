using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DemoApp
{
    /// <summary>
    /// 대리자를 호출하여 다른 객체에 그 기능을 릴레이(연계)하는 것이 유일한 목적입니다.
    /// CanExecute 메서드에 대한 기본 반환 값은 'true'입니다.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        private object p;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// 항상 실행할 수있는 새 명령을 생성합니다.
        /// </summary>
        /// <param name="execute">로직 실행</param>
        public RelayCommand( Action<object> execute )
            : this( execute, null ) { }

        /// <summary>
        /// 항상 실행할 수있는 새 명령을 생성합니다. 조건부 실행
        /// </summary>
        /// <param name="execute">실행 로직</param>
        /// <param name="canExecute">실행 상태 로직</param>
        public RelayCommand( Action<object> execute, Predicate<object> canExecute )
        {
            if( execute == null )
                throw new ArgumentException( nameof( execute ) );

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public bool CanExecute( object parameter )
        {
            return _canExecute == null ? true : _canExecute( parameter );
        }

        public void Execute( object parameter )
        {
            _execute( parameter );
        }

        #endregion // ICommand Members
    }
}
