using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpPost]
        [Route("courier-avaliablity")]
        public async Task<IActionResult> CourierAvaliablity([FromBody] CourierAvailablityRequest model)
        {
            var result = await DispatchService.CourierAvaliablity(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("all-courier")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllCourier()
        {
            var result = await DispatchService.GetAllCourier();
            return result.Any()? Ok(result) : NoContent();
        }


        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("get-all-courier-orders/{courierId}")]
        public async Task<ActionResult<OrderStatus>> GetAllCourierOrders([FromRoute] string courierId)
        {
            var result = await DispatchService.GetAllCourierOrders(courierId);
            return result != null ? Ok(result) : NotFound(); ;
        }


        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("accept-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> AcceptOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.AcceptOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }


        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("reject-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> RejectOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.RejectOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }


        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("start-delivery-order/{orderId}")]
        public async Task<ActionResult<GlobalResponse>> StartDeliveryOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.StartDeliveryOrder(orderId);
            return result.Status == true ? Ok(result) : BadRequest();
        }


        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("end-delivery-order/{orderId}")]
        public async Task<ActionResult<OrderStatus>> EndDeliveryOrder([FromRoute] int orderId)
        {
            var result = await DispatchService.EndDeliveryOrder(orderId);
            return result != null ? Ok(result) : NotFound();
        }

        [Authorize(Roles = UserRoles.Admin_Courier)]
        [HttpGet]
        [Route("get-single-courier/{courierId}")]
        public async Task<ActionResult<GetAllCourierResponse>> GetSingleCourier(string courierId)
        {
            var SingleCourier = await DispatchService.GetSingleCourier(courierId);
            return SingleCourier != null ? Ok(SingleCourier) : NotFound();
        }
    }
}
