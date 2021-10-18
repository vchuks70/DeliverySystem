using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;

namespace Domain.Interface
{
   public interface IDispatch
    {
        Task<IEnumerable<GetAllCourierResponse>> GetAllCourier();
        Task<GetAllCourierResponse> GetSingleCourier(string courierId);
        Task<GlobalResponse> CourierAvaliablity(CourierAvailablityRequest  model);

        Task<IEnumerable<Order>> GetAllCourierOrders(string courierId);
        Task<GlobalResponse> AcceptOrder(int orderId);

        Task<GlobalResponse> RejectOrder(int orderId);

        Task<GlobalResponse> StartDeliveryOrder(int orderId);

        Task<GlobalResponse> EndDeliveryOrder(int orderId);
    }
}
    