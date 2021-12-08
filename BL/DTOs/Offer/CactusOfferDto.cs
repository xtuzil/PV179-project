﻿
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class CactusOfferDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual CactusDto Cactus { get; set; }
        [Required]
        public int CactusId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be minimum of {1}.")]
        [Required]
        public int Amount { get; set; }
        [Required]
        public virtual OfferDto Offer { get; set; }
    }
}
