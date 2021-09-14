using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
  public  class Order: BaseClass
    {
        public ICollection<ProductAndService> ProductAndServices { get; set; }
        public ApplicationUser Customer { get; set; }
        public ApplicationUser Courier { get; set; }

        public string CurrentLocation { get; set; }
            
        public string DestinationLocation { get; set; }

        public DateTime DeliveryCompletedTime { get; set; }

        public DateTime EstimatedDeliveryTime { get; set; }
            
        public bool IsCompleted { get; set; }
        public OrderStatus OrderStatus { get; set; }
            
   
        public decimal ProductTotalPrice { get; set; }

     
        public decimal LogisticsPrice { get; set; } 





        public Order()
        {
            ProductAndServices = new Collection<ProductAndService>();
        }
    }
}
