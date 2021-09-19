using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
   public class CourierAvailablityRequest
    {
        public string CourierId { get; set; }
        public bool IsAvaliable { get; set; }
    }
}
