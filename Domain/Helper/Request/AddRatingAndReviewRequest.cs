using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Request
{
 public   class AddRatingAndReviewRequest
    {
        [Range(1, 5, ErrorMessage = "Rating must be greater than 0 and Less than 6!")]
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }

        public int OrderId { get; set; }
    }
}
