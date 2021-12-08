using BL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class OfferCreateDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int RecipientId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "{0} Can't be negative.")]
        public double? OfferedMoney { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "{0} Can't be negative.")]
        public double? RequestedMoney { get; set; }

        public int? PreviousOfferId { get; set; }
        public List<CactusOfferCreateDto> OfferedCactuses { get; set; }
        public List<CactusOfferCreateDto> RequestedCactuses { get; set; }
    }
}
