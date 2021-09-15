using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class OrderService : IOrder
    {
        private readonly UserManager<ApplicationUser> userManager;

        public OrderService(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            _db = db;
        }

        private readonly ApplicationDbContext _db;
        public async Task<GlobalResponse> AddNewOrder(AddOrder model)
        {
            var customer = await userManager.FindByIdAsync(model.CustomerId);
            if(customer is null)
            {
                return new GlobalResponse { Status = false, Message = "Customer not found" };
            }

            var courier = await userManager.FindByIdAsync(model.CourierId);
            if (courier is null)
            {
                return new GlobalResponse { Status = false, Message = "Courier not found" };
            }

            var orderStatus = await _db.OrderStatuses.SingleOrDefaultAsync(x => x.Id ==model.OrderStatusId);
            if (orderStatus is null)
            {
                return new GlobalResponse { Status = false, Message = "Order Status not found" };
            }

            var order = new Order
            {
                Customer = customer,
                Courier = courier,
                OrderStatus = orderStatus,
                CurrentLocation = model.CurrentLocation,
                DeliveryCompletedTime = null,
                DestinationLocation = model.DestinationLocation,
                EstimatedDeliveryTime = model.EstimatedDeliveryTime,
                IsCompleted = false,
              
                LogisticsPrice = model.LogisticsPrice,
                OrderTime = DateTime.Now,
                ProductTotalPrice = 0
            };

            foreach (var item in model.ProductAndServices)
            {
                var Product = await _db.ProductAndServices.SingleOrDefaultAsync(x => x.Id == item.ProductServiceId);
                if (Product is null)
                {
                    return new GlobalResponse { Status = false, Message = $"Product with the id {item.ProductServiceId} not found" };
                }
                var newOrderProduct = new OrderProduct
                {
                    ProductAndServices = Product,
                    PriceWhenOrdered = Product.Price
                };
                order.OrderProduct.Add(newOrderProduct);
            }

            order.ProductTotalPrice = order.OrderProduct.Sum(x => x.PriceWhenOrdered);

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();


            return new GlobalResponse { Status = true, Message = $"Successful" };
        }
    }
}

