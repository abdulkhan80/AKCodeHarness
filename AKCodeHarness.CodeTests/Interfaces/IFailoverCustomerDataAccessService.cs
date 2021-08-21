using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    public interface IFailoverCustomerDataAccessService
    {
        Task<CustomerResponse> GetCustomerById(int id);
    }
}