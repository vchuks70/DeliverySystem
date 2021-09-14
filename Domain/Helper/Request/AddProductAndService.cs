using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
  public  class AddProductAndService
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public float DiscountPercentage { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }

        [Required]
        public int InventoryCount { get; set; }
    }
}
