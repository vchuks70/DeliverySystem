using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        public IDispatch DispatchService { get; set; }

        public DispatchController(IDispatch dispatchService)
        {
            DispatchService = dispatchService;
        }

        [HttpPost]
        [Route("courier-avaliablity")]
        public async Task<IActionResult> CourierAvaliablity([FromBody] CourierAvailablityRequest model)
        {
            var result = await DispatchService.CourierAvaliablity(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("all-courier")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllCourierOrders()
        {
            var result = await DispatchService.GetAllCourier();
            return result.Any()? Ok(result) : NoContent();
        }

        [HttpGet]
        [Route("get-all-courier-orders/{courierId}")]
        public async Task<ActionResult<OrderStatus>> GetAllCourierOrders([FromRoute] string courierId)
        {
            var result = await DispatchService.GetAllCourierOrders(courierId);
            return result != null ? Ok(result) : NotFound(); ;
        }
        [HttpGet]
        [Route("accept-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> AcceptOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.AcceptOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet]
        [Route("reject-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> RejectOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.RejectOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [Route("start-delivery-order/{orderId}")]
        public async Task<ActionResult<GlobalResponse>> StartDeliveryOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.StartDeliveryOrder(orderId);
            return result.Status == true ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [Route("end-delivery-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> EndDeliveryOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.EndDeliveryOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
