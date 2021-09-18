using Data;
using Domain.Helper.Request;
using Domain.Helper.Response;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ReportService : IReport
    {
        private readonly ApplicationDbContext _db;

        public ReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> GetDailyOrders(GetReportOrdersRequest model)
        {
            var response = await _db.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderProduct)
                .Include(x => x.OrderStatus)
                .Include(x => x.Courier)
                .Where(x => x.OrderTime >= model.DateFrom && x.OrderTime <= model.DateTo)
                .Take(model.Count)
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<MostPopularOrderResponse>> GetMostPopularOrder(GetReportOrdersRequest model)
        {
            var response = await _db.OrderProduct
              .Include(x => x.ProductAndServices)
              .Where(x => x.OrderTime >= model.DateFrom && x.OrderTime <= model.DateTo)
              .ToListAsync();

            var result = new List<MostPopularOrderResponse>();

            foreach (var item in response)
            {
                var check = result.FirstOrDefault(x => x.ProductId == item.ProductAndServices.Id);

                if (check is null)
                {
                    result.Add(new MostPopularOrderResponse
                    { PurchaseCount = 1, ProductId = item.ProductAndServices.Id, ProductName = item.ProductAndServices.Name });
                }
                else
                {
                    check.PurchaseCount++;
                }
            }

          var ordered =   result.OrderByDescending(x => x.PurchaseCount).Take(model.Count).ToList();
            return ordered;

            //var rr  = from val in response
            //          group val by val.ProductAndServices into gp
            //          select new MostPopularOrderResponse
            //          {
            //           ProductName = gp.Key.Name,
            //           PurchaseCount = gp.Count()
                       
            //          }
            //var group = response.GroupBy(x => x.ProductAndServices).ToList();
            //var ee = group.OrderByDescending( x=> x.Key).
           


        }

        public async Task<IEnumerable<TargetLocationsResponse>> GetMostPopularTargetLocationsResponse(GetReportOrdersRequest model)
        {
            var response = await _db.Orders
             
                 .Where(x => x.OrderTime >= model.DateFrom && x.OrderTime <= model.DateTo).GroupBy(x =>x.DestinationLocation)
                 .OrderByDescending(x => x.Count())
                 .Take(model.Count)
                 .Select(x => new TargetLocationsResponse { Location = x.Key, Count = x.Count()})
                 .ToListAsync();

            return response;
        }
    }
}
