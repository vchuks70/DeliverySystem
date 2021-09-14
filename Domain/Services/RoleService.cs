using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
   public class RoleService: IRole
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _db;

        public RoleService(RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            this.roleManager = roleManager;
            _db = db;
        }

        public async Task<GlobalResponse> AddRole(RoleRequest model)
        {
            var check = await roleManager.RoleExistsAsync(model.RoleName);
            if (check)
            {
                return new GlobalResponse { Status = false, Message = "Role already exist" };
            }

           var result =   await roleManager.CreateAsync(new IdentityRole(model.RoleName));

            if (result.Succeeded)
            {
                await _db.SaveChangesAsync();
                return new GlobalResponse { Status = true, Message = "Role created successfully" };
            }

            return new GlobalResponse { Status = false, Message = "Error, Please try again" };
        }

        public async Task<IEnumerable<RoleListResponse>> GetRoles()
        {
            var result = await roleManager.Roles.Where(x => x.Name != "Admin").Select( x=>  new RoleListResponse { RoleId = x.Id, RoleName = x.Name}).ToListAsync();
            return result;
        }
    }
}
