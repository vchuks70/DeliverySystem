using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
            private readonly ApplicationDbContext _db;

        public CustomerService( ApplicationDbContext db )
        {

            _db = db;

        }
               
            public async Task<IEnumerable<GetAllCustomersResponse>> GetAllCustomer()
        {
            var customers = await (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == UserRoles.Customer) on userRoles.RoleId equals roles.Id
                            select new GetAllCustomersResponse
                            {
                                Id = user.Id,
                                Name = user.Name,
                                PhoneNumber = user.PhoneNumber,
                                Email = user.Email
                            }
                        ).ToListAsync();

            return customers;
        }
    }
}
