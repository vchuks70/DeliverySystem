using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;

namespace Domain.Interface
{
    public interface IRoute
    {
        Task<GlobalResponse> AddRoute(RouteRequest route);
        Task<IEnumerable<GlobalResponse>> GetAllRoute();
    }
}
