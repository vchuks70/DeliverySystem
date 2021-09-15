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
        [Route("AddOrderStatus")]
        public async Task<IActionResult> AddOrderStatus([FromBody] OrderStatusRequest model)
        {
            var result = await OrderService.AddOrderStatus(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }
    }
}
