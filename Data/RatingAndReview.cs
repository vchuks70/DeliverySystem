using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
  public  class RatingAndReview : BaseClass
    {
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be greater than 0 and Less than 6!")]
        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }

        [JsonIgnore]
        public int OrderId { get; set; }
        [JsonIgnore]
        [ForeignKey("OrderId")]
        public Order  Order { get; set; }



    }
}
