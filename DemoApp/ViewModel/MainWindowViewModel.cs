using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// 응용 프로그램의 Main Window를 위한 ViewModel
    /// </summary>
    public class MainWindowViewModel : CommandViewModel
    {
        #region Fields

        ReadOnlyCollection<ActionViewModel> _commands;
        readonly CustomerRepository _customerRepository;
        ObservableCollection<CommandViewModel> _commandViewModel;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel( string customerDataFile )
        {
            base.DisplayName = Strings.MainWindowViewModel_DisplayName;
            _customerRepository = new CustomerRepository( customerDataFile );
        }

        #endregion
    }
}
