using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
  public  class GetReportOrdersRequest   
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Count { get; set; }
    }
}
    