using BL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class OfferDto
    {
        public int Id { get; set; }
        public UserInfoDto Author { get; set; }
        public int AuthorId { get; set; }
        public UserInfoDto Recipient { get; set; }
        public int RecipientId { get; set; }
        public double? OfferedMoney { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime? ResponseDate { get; set; }

        public OfferDto PreviousOffer { get; set; }
        public DateTime CreationDate { get; set; }

        public List<CactusOfferDto> OfferedCactuses { get; set; }
        public List<CactusOfferDto> RequestedCactuses { get; set; }

        public TransferDto Transfer { get; set; }
    }
}
