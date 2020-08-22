using Asos.CodeTest.Types;
using Asos.CodeTest.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asos.CodeTest.Services
{
    public class ValidationService : IValidationService
    {
        private readonly Dictionary<CustomerStore, IStoreValidator> _storeValidation;

        public ValidationService()
        {
            _storeValidation = new Dictionary<CustomerStore, IStoreValidator>()
            {
                { CustomerStore.ArchivedStore, new CustomerArchivedStoreValidator()},
                { CustomerStore.CustomerStore, new CustomerStoreValidator()},
                { CustomerStore.FailOverStore, new CustomerFailOverStoreValidator()}
            };
        }

        public CustomerStoreResults  Validate(CustomerStore customerStore,Customer customer,CustomerStoreRequest customerStoreRequest)
        {
            return _storeValidation[customerStore].IsValidStore(customer, customerStoreRequest);
        }
    }
}
