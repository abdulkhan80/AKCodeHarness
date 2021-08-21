using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    public interface ICustomerDataAccess
    {
        Task<CustomerResponse> LoadCustomerAsync(int customerId);
    }
}