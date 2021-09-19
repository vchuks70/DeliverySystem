using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Response
{
  public  class GetAllCourierResponse
    {
        public string CourierId { get; set; }
        public string CourierName { get; set; }
        public string CourierPhoneNumber { get; set; }
        public int DeliveryCount { get; set; }
      //  public int AverageDeliveryTime { get; set; }
    }
    }
        