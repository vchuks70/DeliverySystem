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
    public interface IOrder
    {
        Task<GlobalResponse> AddNewOrder(AddOrder model);
        //Task<GlobalResponse> AddOrderStatus(OrderStatusRequest model);
        //Task <OrderStatus> GetOrderStatus(int orderStatusId);
        Task<GlobalResponse> DeleteOrder(int orderId);
        Task<GlobalResponse> CompleteOrder(int orderId);

        Task<Order> GetOrder(int orderId);

        Task<Order> PaymentOrder(int orderId);

        Task<OrderCourierResponse> GetOrderCourier(int orderId);


        Task<GlobalResponse> AddRatingAndReview(AddRatingAndReviewRequest model);
        Task<RatingAndReview> GetOrderRatingAndReview(int orderId);



        Task<GlobalResponse> ReasignOrder(ReasignOrderRequest model);
    }
}
