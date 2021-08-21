using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(int customerId, bool isCustomerArchived);
    }
}
