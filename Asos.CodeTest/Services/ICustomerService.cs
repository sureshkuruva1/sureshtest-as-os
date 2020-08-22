using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asos.CodeTest.Services
{
    interface ICustomerService
    {
        Task<Customer> GetCustomer(int customerId, bool isCustomerArchived);
    }
}
