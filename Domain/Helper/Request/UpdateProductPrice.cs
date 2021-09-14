﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
  public  class UpdateProductPrice
    {
        [Required]
        public int  ProductId { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
