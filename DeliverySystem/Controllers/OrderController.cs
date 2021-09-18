using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrder OrderService { get; set; }

        public OrderController(IOrder orderService)
        {
            OrderService = orderService;
        }

        [HttpPost]
        [Route("AddNewOrder")]
        public async Task<IActionResult> AddNewOrder([FromBody] AddOrder model)
        {
            var result = await OrderService.AddNewOrder(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("add/order-status")]
        public async Task<IActionResult> AddOrderStatus([FromBody] OrderStatusRequest model)
        {
            var result = await OrderService.AddOrderStatus(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("order-status/{orderStatusId}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus([FromRoute] int orderStatusId)
        {
            var result = await OrderService.GetOrderStatus(orderStatusId);
            return result != null ? Ok(result) : NotFound(); ;
        }

        [HttpDelete]
        [Route("delete-order/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int orderId)
        {
            var result = await OrderService.DeleteOrder(orderId);
            return result.Status == true ? Ok(result) : NotFound(); 
        }

        [HttpGet]
        [Route("complete-order/{orderId}")]
        public async Task<IActionResult> CompleteOrder([FromRoute] int orderId)
        {
            var result = await OrderService.CompleteOrder(orderId);
            return result.Status == true ? Ok(result) : NotFound(); 
        }

        [HttpGet]
        [Route("courier/{orderId}")]
        public async Task<IActionResult> GetOrderCourier([FromRoute] int orderId)
        {
            var result = await OrderService.GetOrderCourier(orderId);
            return result != null ? Ok(result) : NotFound(); 
        }

        [HttpGet]
        [Route("get-order/{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            var result = await OrderService.GetOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
