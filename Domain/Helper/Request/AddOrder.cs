using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
    public class AddOrder
    {
        public ICollection<ProductServiceRequestInfo> ProductAndServices { get; set; }
        public string CustomerId { get; set; }
        public string CourierId { get; set; }

        public string CurrentLocation { get; set; }

        public string DestinationLocation { get; set; }

        public DateTime DeliveryCompletedTime { get; set; }

        public DateTime EstimatedDeliveryTime { get; set; }

        public bool IsCompleted { get; set; }
        public int OrderStatusId { get; set; }


        public decimal ProductTotalPrice { get; set; }


        public decimal LogisticsPrice { get; set; }
    }

    public class ProductServiceRequestInfo 
    {
        public int ProductServiceId { get; set; }
        

    }


}
