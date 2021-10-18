using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Helper.Response;
using Domain.Interface;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService CustomerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            CustomerService = customerService;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllCustomers()
        {
            var result = await CustomerService.GetAllCustomer();
            return result.Any() ? Ok(result) : NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("get-single-customer/{customerId}")]

        public async Task<ActionResult<GetAllCustomersResponse>> GetSingleCustomer(string customerId)
        {
            var SingleCustomer = await CustomerService.GetSingleCustomer(customerId);
            return SingleCustomer != null ? Ok(SingleCustomer) : NotFound();
        }
    }
}
