using BL.Enums;
using System;
using System.Collections.Generic;

namespace BL.DTOs
{
    public class OfferCreateDto
    {
        public int AuthorId { get; set; }

        public int RecipientId { get; set; }
        public double? OfferedMoney { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public int? PreviousOfferId { get; set; }
        public List<CactusOfferCreateDto> OfferedCactuses { get; set; }
        public List<CactusOfferCreateDto> RequestedCactuses { get; set; }
    }
}
