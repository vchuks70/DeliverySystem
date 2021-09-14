using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Response
{
   public class EditProductAndService
    {
        public int ProductAndServiceId { get; set; }
        public string Name { get; set; }


        public decimal Price { get; set; }


        public float DiscountPercentage { get; set; }

        public int ProductCategoryId { get; set; }
        public int InventoryCount { get; set; }
    }
}
