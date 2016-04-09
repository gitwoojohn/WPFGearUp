using System;
using DemoApp.Model;

namespace DemoApp.DataAccess
{
    /// <summary>
    /// CustomerRepository's의 CustomerAdded 이벤트 인수입니다.
    /// </summary>
    public class CustomerAddedEventArgs : EventArgs
    {
        public CustomerAddedEventArgs( Customer newCustomer )
        {
            this.NewCustomer = newCustomer;
        }

        public Customer NewCustomer { get; private set; }
    }
}
