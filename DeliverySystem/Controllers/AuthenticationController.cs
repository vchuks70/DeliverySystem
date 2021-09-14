using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        

        public IAuthentication AuthenticationService { get; set; }

        public AuthenticationController(IAuthentication authenticationService)
        {
            AuthenticationService = authenticationService;
        }

      

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) {

            var result = await AuthenticationService.Register(model);
            return result.Status == true ? Ok(result) : BadRequest(result);


        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>Login([FromBody] LoginModel model)
        {
            var result = await AuthenticationService.Login(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminModel model)
        {
            var result = await AuthenticationService.RegisterAdmin(model);
            return result.Status == true ? Ok(result) : BadRequest(result);
        }
    }
}
