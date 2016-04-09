using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// 뷰에 의해 표시되는 실행 가능한 항목을 나타냅니다.
    /// (Represents an actionable item displayed by a View.)
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel( string displayName, ICommand command )
        {
            if( command == null )
                throw new ArgumentNullException( "command" );

            base.DisplayName = displayName;
            this.Command = command;
        }

        public ICommand Command { get; private set; }
    }
}
