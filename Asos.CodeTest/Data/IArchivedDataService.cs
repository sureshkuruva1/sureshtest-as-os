using System;
using System.Collections.Generic;
using System.Text;

namespace Asos.CodeTest.Data
{
    public interface IArchivedDataService
    {
        Customer GetArchivedCustomer(int customerId);
    }
}
