using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
  public  class ReasignOrderRequest
    {
        public int OrderId { get; set; }
        public string CourierId { get; set; }
    }
}
