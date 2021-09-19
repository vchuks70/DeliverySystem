using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayStackDotNetSDK.Methods.Transactions;
using PayStackDotNetSDK.Models.Transactions;

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
		[Route("order-payment/{orderId}")]
		public async Task<IActionResult> TestPay([FromRoute] int  orderId)
		{
			var response = await OrderService.PaymentOrder(orderId);

            if (response is null)
            {
				return BadRequest();
            }

			var result = await InitializeTransaction(response);
			return Ok(new { ReturnUrl = result});

		}

		[HttpPost]
		[Route("AddNewOrder")]
		public async Task<IActionResult> AddNewOrder([FromBody] AddOrder model)
		{
			var result = await OrderService.AddNewOrder(model);
			return result.Status == true ? Ok(result) : BadRequest(result);
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

		[HttpPost]
		[Route("add/rating-and-review")]
		public async Task<IActionResult> AddRatingAndReview([FromBody] AddRatingAndReviewRequest model)
		{
			var result = await OrderService.AddRatingAndReview(model);
			return result.Status == true ? Ok(result) : BadRequest(result);
		}
			
		[HttpGet]
		[Route("rating-and-review/{orderId}")]
		public async Task<ActionResult<OrderStatus>> GetOrderRatingAndReview([FromRoute] int orderId)
		{
			var result = await OrderService.GetOrderRatingAndReview(orderId);
			return result != null ? Ok(result) : NotFound(); ;
		}


		[HttpPost]
		[Route("reasign-order")]
		public async Task<IActionResult> AddRatingAndReview([FromBody] ReasignOrderRequest model)
		{
			var result = await OrderService.ReasignOrder(model);
			return result.Status == true ? Ok(result) : BadRequest(result);
		}

		/// <summary>
	/// Implements simple InitializeTransaction with basic parameters
	/// </summary>
	protected async Task<string> InitializeTransaction(Order order)
	{
		var connectionInstance = new PaystackTransaction("sk_test_a09869ed06050dc55e364b325b139a6ee1fb1586");
			// var response = await connectionInstance.InitializeTransaction("vchuks70@gmail.com", 11000);

			var response = await connectionInstance.InitializeTransaction(
			new TransactionRequestModel()
			{
				firstName = order.Customer.Name,
				lastName = order.Customer.Name,
				amount = (int)order.ProductTotalPrice + (int)order.LogisticsPrice,
				currency = PayStackDotNetSDK.Helpers.Constants.Currency.Naira,
				email = order.Customer.Email,
				transaction_charge = 0
			}
			);
		if (response.status)
		{
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Response.Redirect(response.data.authorization_url); //Redirects your browser to the secure URL
				return response.data.authorization_url;
		}
		else //not successful
		{
				//Do something else with the info.
			return	null;
		}
	}


    }
}
