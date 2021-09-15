using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;

namespace Domain.Interface
{
    public interface IOrder
    {
        Task<GlobalResponse> AddNewOrder(AddOrder model);

    }
}
