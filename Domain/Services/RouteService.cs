using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliverySystem.Helper.Response;
using Domain.Helper.Request;
using Domain.Interface;

namespace Domain.Services
{
    public class RouteService : IRoute
    {
        public Task<GlobalResponse> AddRoute(RouteRequest route)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GlobalResponse>> GetAllRoute()
        {
            throw new NotImplementedException();
        }
    }
}
