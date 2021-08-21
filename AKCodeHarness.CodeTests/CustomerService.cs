using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKCodeHarness.CodeTests
{
    public class CustomerService : ICustomerService
    {
        private readonly IArchivedDataService _archivedDataService;
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly IFailoverService _failoverService;
        private readonly IFailoverCustomerDataAccessService _failoverCustomerDataAccessService;

        public CustomerService(IArchivedDataService archivedDataService, ICustomerDataAccess customerDataAccess
                                , IFailoverService failoverService, IFailoverCustomerDataAccessService failoverCustomerDataAccessService)
        {
            this._archivedDataService = archivedDataService;
            this._customerDataAccess = customerDataAccess;
            this._failoverService = failoverService;
            this._failoverCustomerDataAccessService = failoverCustomerDataAccessService;
        }

        public CustomerService(){}

        public async Task<Customer> GetCustomer(int customerId, bool isCustomerArchived)
        {

            Customer archivedCustomer = null;

            if (isCustomerArchived)
            {
                archivedCustomer = this._archivedDataService.GetArchivedCustomer(customerId);

                return archivedCustomer;
            }

            CustomerResponse customerResponse = null;
            Customer customer = null;

            if (this._failoverService.OnFailover())
            {
                customerResponse = await this._failoverCustomerDataAccessService.GetCustomerById(customerId);
            }
            else
            {
                customerResponse = await _customerDataAccess.LoadCustomerAsync(customerId);
            }

            if (customerResponse.IsArchived)
            {
                customer = this._archivedDataService.GetArchivedCustomer(customerId);
            }
            else
            {
                customer = customerResponse.Customer;
            }


            return customer;
        }
    }
}
