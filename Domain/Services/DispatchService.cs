using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.Services
{
    public class DispatchService : IDispatch
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _db;

        public DispatchService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            this.userManager = userManager;
            _db = db;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }
        public async Task<GlobalResponse> CourierAvaliablity(CourierAvailablityRequest model)
        {
            var courier = await userManager.FindByIdAsync(model.CourierId);
            if (courier is null)
            {
                return new GlobalResponse { Status = false, Message = "Courier not found" };
            }
            courier.IsAvaliable = model.IsAvaliable;
            await _db.SaveChangesAsync();


            return new GlobalResponse { Status = true, Message = $"Successful" };
        }

        public async Task<IEnumerable<GetAllCourierResponse>> GetAllCourier()
        {

            var result = await _db.Orders
                .Include(x => x.Courier)
                .Where(x => x.IsCompleted == true)
                .GroupBy(x => x.Courier)
                .Select(y => new GetAllCourierResponse {
                    CourierId = y.Key.Id,
                    CourierName = y.Key.Name,
                    CourierPhoneNumber = y.Key.PhoneNumber,
                    DeliveryCount = y.Count(),
                }).ToListAsync();

            return result;

            //                    AverageDeliveryTime = y.Sum(x => x.DeliveryCompletedTime.Value.Subtract(x.EstimatedDeliveryTime.Value))
            //var couriers= await userManager.Users.Where(x => x.IsCourier == true).ToListAsync();

            //var rr = from courier in userManager.Users
            //         join ee in 
            //return couriers;
        }

        public async Task<IEnumerable<Order>> GetAllCourierOrders(string courierId)
        {
            var GetOrder = await _db.Orders
                .Include(x => x.Customer)
               .Include(x => x.OrderProduct)
               .Include(x => x.OrderStatus)
               .Include(x => x.Courier)
               .Where(x => x.Courier.Id == courierId).ToListAsync();


            return GetOrder;
        }

        public async Task<GlobalResponse> AcceptOrder(int orderId)
        {

            var order = await _db.Orders.Include(x=> x.Courier).FirstOrDefaultAsync(x => x.Id == orderId);
            if (order is null)
            {
                return new GlobalResponse { Status = false, Message = "Order not found" };
            }

            if (order.Courier is null)
            {
                return new GlobalResponse { Status = false, Message = "Order Courier not found" };
            }

            order.IsAccepted = true;
            order.OrderStatus = "Order Accepted By Courier";

            await _db.SaveChangesAsync();

            return new GlobalResponse { Status = true, Message = "Successful" };


        }

        public async Task<GlobalResponse> RejectOrder(int orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null)
            {
                return new GlobalResponse { Status = false, Message = "Order not found" };
            }

            order.IsAccepted = false;
            order.OrderStatus = "Order Placed, Awaiting Courier";
            order.Courier = null;


            await _db.SaveChangesAsync();

            return new GlobalResponse { Status = true, Message = "Successful" };
        }

     
        public async Task<GlobalResponse> StartDeliveryOrder(int orderId)
        {

            var order = await _db.Orders
                .Include(x => x.Customer).Include(x => x.Courier).FirstOrDefaultAsync(x => x.Id == orderId);
            if (order is null)
            {
                return new GlobalResponse { Status = false, Message = "Order not found" };
            }

            if (order.IsAccepted == false)
            {
                return new GlobalResponse { Status = false, Message = "Order not accepted by a courier" };
            }
            order.OrderStatus = "Order In Transit";

            sendAcceptanceEmail(order.Customer, order.Courier);
            await _db.SaveChangesAsync();

            return new GlobalResponse { Status = true, Message = "Successful" };
        }

        public async Task<GlobalResponse> EndDeliveryOrder(int orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order is null)
            {
                return new GlobalResponse { Status = false, Message = "Order not found" };
            }

            if (order.IsAccepted == false)
            {
                return new GlobalResponse { Status = false, Message = "Order not accepted by a courier" };
            }
            order.OrderStatus = "Order Completed";
            order.IsCompleted = true;
            order.DeliveryCompletedTime = DateTime.Now;

            await _db.SaveChangesAsync();

            return new GlobalResponse { Status = true, Message = "Successful" };
        }

        private void sendAcceptanceEmail(ApplicationUser user, ApplicationUser courier)
        {
            string message = $@"<p>Order in transit</p>
                             <p> Courier Name: {courier.Name}</p>
                              <p> Courier Name: {courier.PhoneNumber}</p>";


            _emailService.Send(
                to: user.Email,
                subject: "Order Accepted By Courier",
                html: $@"<h4>Order In Transit</h4>
                         <p>Thanks for shopping with us!</p>
                         {message}"
            );


        }

    }
}
