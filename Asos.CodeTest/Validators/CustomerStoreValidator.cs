using Asos.CodeTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asos.CodeTest.Validators
{
    public class CustomerStoreValidator : StoreValidatorBase
    {
        public override CustomerStoreResults IsValidStore(Customer customer, CustomerStoreRequest customerStoreRequest)
        {
            if (!IsValidCustomer(customer))
            {
                return new CustomerStoreResults { Success = false };
            }
            return new CustomerStoreResults() { Success = customer.customerStoreTypes.HasFlag(CustomerStore.FailOverStore) };
        }
    }
}
