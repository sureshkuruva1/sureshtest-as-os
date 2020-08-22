using Asos.CodeTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asos.CodeTest.Validators
{
    public interface IStoreValidator
    {
        CustomerStoreResults IsValidStore(Customer customer, CustomerStoreRequest customerStoreRequest);
    }
}
