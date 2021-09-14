using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using DeliverySystem.Helper.Response;
using Domain.Helper.Response;

namespace Domain.Interface
{
    public interface IAuthentication
    {
        public Task<GlobalResponse> Register(RegisterModel model);
        public Task<LoginResponse> Login(LoginModel model);
        public Task<GlobalResponse> RegisterAdmin(RegisterAdminModel model);

      }
}
