using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helper.Response;

namespace Domain.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetAllCustomersResponse>> GetAllCustomer();
        Task<GetAllCustomersResponse> GetSingleCustomer(string customerId);
    }
}
