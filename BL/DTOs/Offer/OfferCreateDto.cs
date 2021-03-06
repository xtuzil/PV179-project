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
        [Range(0, int.MaxValue, ErrorMessage = "Offered money cannot be negative.")]
        public double? OfferedMoney { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Requested money cannot be negative.")]
        public double? RequestedMoney { get; set; }

        public int? PreviousOfferId { get; set; }
        public Dictionary<int, int> OfferedCactuses { get; set; }
        public Dictionary<int, int> RequestedCactuses { get; set; }
    }
}
