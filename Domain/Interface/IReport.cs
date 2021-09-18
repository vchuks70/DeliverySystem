using Data;
using Domain.Helper.Request;
using Domain.Helper.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
  public  interface IReport
    {
        Task<IEnumerable<MostPopularOrderResponse>> GetMostPopularOrder(GetReportOrdersRequest model);
        Task<IEnumerable<TargetLocationsResponse>> GetMostPopularTargetLocationsResponse(GetReportOrdersRequest model);

        Task<IEnumerable<Order>> GetDailyOrders(GetReportOrdersRequest  model);




    }
}
