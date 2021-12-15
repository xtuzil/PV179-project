using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CactusOfferUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CactusId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be minimum of {1}.")]
        [Required]
        public int Amount { get; set; }
        [Required]
        public int OfferId { get; set; }
    }
}
