using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class ProductAndService: BaseClass
    {
        public string Name { get; set; }

       
        public decimal Price { get; set; }


        public float DiscountPercentage { get; set; }

        public ProductCategory ProductCategoryName { get; set; }
        public int InventoryCount { get; set; }
    }
}
