using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IRole RoleService { get; set; }

        public RoleController(IRole roleService)
        {
            RoleService = roleService;
        }



        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Add")]
        public async Task<IActionResult> Register([FromBody] RoleRequest  model)
        {

            var result = await RoleService.AddRole(model);
            return result.Status == true ? Ok(result) : BadRequest(result);


        }

        [HttpGet]
        [Route("all-roles")]
        public async Task<IActionResult> GetAll()
        {

            var result = await RoleService.GetRoles();
            return result.Any() == true ? Ok(result) : NoContent();


        }
    }
}
