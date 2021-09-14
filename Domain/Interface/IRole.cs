using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Helper.Response;

namespace Domain.Interface
{
   public interface IRole
    {
        Task<GlobalResponse> AddRole(RoleRequest model);
        Task<IEnumerable<RoleListResponse>> GetRoles();
    }
}
