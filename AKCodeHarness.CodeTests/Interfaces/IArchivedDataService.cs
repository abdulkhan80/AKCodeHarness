using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    public interface IArchivedDataService
    {
        Customer GetArchivedCustomer(int customerId);
    }
}
