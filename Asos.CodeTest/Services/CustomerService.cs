using Asos.CodeTest.Services;
using System;
using System.Threading.Tasks;

namespace Asos.CodeTest
{
    public class CustomerService : ICustomerService
    {
        private readonly IAppSettings _settings;
        private readonly IFailoverRepository _failoverRepository;
        private readonly ICustomerDataAccess _customerDataAccess;

        public CustomerService(IAppSettings settings, IFailoverRepository failoverRepository, ICustomerDataAccess customerDataAccess)
        {
            _settings = settings;
            _failoverRepository = failoverRepository;
            _customerDataAccess = customerDataAccess;
        }

        public async Task<Customer> GetCustomer(int customerId, bool isCustomerArchived)
        {

            Customer archivedCustomer = null;

            if (isCustomerArchived)
            {
                var archivedDataService = new ArchivedDataService();
                archivedCustomer = archivedDataService.GetArchivedCustomer(customerId);

                return archivedCustomer;
            }
            else
            {
                var failoverEntries = _failoverRepository.GetFailOverEntries();


                var failedRequests = 0;

                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }

                CustomerResponse customerResponse = null;
                Customer customer = null;

                if (failedRequests > 100 && _settings.IsFailoverModeEnabled)
                {
                    customerResponse = await FailoverCustomerDataAccess.GetCustomerById(customerId);
                }
                else
                {
                    customerResponse = await _customerDataAccess.LoadCustomerAsync(customerId);


                }

                if (customerResponse.IsArchived)
                {
                    var archivedDataService = new ArchivedDataService();
                    customer = archivedDataService.GetArchivedCustomer(customerId);
                }
                else
                {
                    customer = customerResponse.Customer;
                }


                return customer;
            }
        }
    }
}
