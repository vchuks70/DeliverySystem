using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using DeliverySystem.Helper.Response;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext db, IJwtGenerator jwtGenerator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _db = db;
            _jwtGenerator = jwtGenerator;
        }



        public async Task<GlobalResponse> Register(RegisterModel model)
        {
            var userExist = await userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
                return new GlobalResponse { Status = false, Message = "User Already Exist" };

            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role is null)
            {
                return new GlobalResponse { Status = false, Message = "Role does not exit" };
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name.Replace(" ", ""),
                Name = model.Name,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded == false)
            {
                return new GlobalResponse { Status = false, Message = "User Creation Failed" };
            }

       

            await userManager.AddToRoleAsync(user, role.Name);

            await _db.SaveChangesAsync();
            return new GlobalResponse { Status = true, Message = "User Created Successfully" };
        }


        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return new LoginResponse { Status = false, Message = "Failed, User not found", Token = null };
            }
            var check = await userManager.CheckPasswordAsync(user, model.Password);
            if (check)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var token = _jwtGenerator.CreateToken(user, userRoles.FirstOrDefault());
                return new LoginResponse { Status = true, Message = "Login Successful", Token = token };
            }

            return new LoginResponse { Status = false, Message = "Login Failed, Invalid Password", Token = null };
        }

        public async Task<GlobalResponse> RegisterAdmin(RegisterAdminModel model)
        {

            var userExist = await userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
                return new GlobalResponse { Status = false, Message = "User Already Exist" };

         

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name.Replace(" ", ""),
                Name = model.Name,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded == false)
            {
                return new GlobalResponse { Status = false, Message = "User Creation Failed" };
            }

            var check = await roleManager.RoleExistsAsync(model.Role);
            if (check == false)
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }
         
                await userManager.AddToRoleAsync(user, model.Role);
            

            await _db.SaveChangesAsync();
            return new GlobalResponse { Status = true, Message = "User Created Successfully" };
        }
    }
}
