using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class CactusOfferCreateDto
    {
        [Required]
        public virtual int CactusId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "{0} should be minimum of {1}.")]
        [Required]
        public int Amount { get; set; }
        [Required]
        public virtual int OfferId { get; set; }
    }
}
