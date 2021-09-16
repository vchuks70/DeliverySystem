using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;

namespace Domain.Interface
{
    public interface IOrder
    {
        Task<GlobalResponse> AddNewOrder(AddOrder model);
        Task<GlobalResponse> AddOrderStatus(OrderStatusRequest model);
        Task <OrderStatus> GetOrderStatus(int CustomerId);
        Task<GlobalResponse> DeleteOrder(int CustomerId);

    }
}
