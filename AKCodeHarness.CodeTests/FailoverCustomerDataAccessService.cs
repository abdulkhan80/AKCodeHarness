using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    /*  Abdul Khan: 31/07/18 14:00

    1) FailoverCustomerDataAccessService class make us easy to Mock static FailoverCustomerDataAccess class.
    */

    public class FailoverCustomerDataAccessService : IFailoverCustomerDataAccessService
    {

        public async Task<CustomerResponse> GetCustomerById(int id)
        {
            return await FailoverCustomerDataAccess.GetCustomerById(id);
        }

    }
}
