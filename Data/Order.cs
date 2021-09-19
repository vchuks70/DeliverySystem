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
        public Order()
        {
            OrderProduct = new Collection<OrderProduct>();
        }
 
    public ICollection<OrderProduct > OrderProduct { get; set; }
        public ApplicationUser Customer { get; set; }
        public ApplicationUser? Courier { get; set; }

        public string CurrentLocation { get; set; }
            
        public string DestinationLocation { get; set; }

        public DateTime? DeliveryCompletedTime { get; set; }

        public DateTime? EstimatedDeliveryTime { get; set; }
            
        public bool IsCompleted { get; set; }
        public string OrderStatus { get; set; }
            
   
        public decimal ProductTotalPrice { get; set; }

     
        public decimal LogisticsPrice { get; set; }

        public DateTime OrderTime { get; set; }

        public bool IsAccepted { get; set; } = false;






    }
    
    public class OrderProduct: BaseClass
    {
      
        public ProductAndService ProductAndServices { get; set; }
        public decimal PriceWhenOrdered { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
