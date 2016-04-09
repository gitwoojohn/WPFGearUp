using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataAccess
{
    class CustomerRepository
    {
        private string customerDataFile;

        public CustomerRepository( string customerDataFile )
        {
            this.customerDataFile = customerDataFile;
        }
    }
}
