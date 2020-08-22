using Asos.CodeTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asos.CodeTest.Validators
{
    public abstract class StoreValidatorBase : IStoreValidator
    {
        protected static bool IsValidCustomer(Customer customer)
        {
            return customer != null;
        }

        public abstract CustomerStoreResults IsValidStore(Customer customer, CustomerStoreRequest customerStore);
    }
}
