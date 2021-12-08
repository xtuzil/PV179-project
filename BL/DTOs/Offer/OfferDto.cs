using BL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class OfferDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public UserInfoDto Author { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public UserInfoDto Recipient { get; set; }
        [Required]
        public int RecipientId { get; set; }
        public double? OfferedMoney { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public OfferDto PreviousOffer { get; set; }
        public DateTime CreationDate { get; set; }

        public List<CactusOfferDto> OfferedCactuses { get; set; }
        public List<CactusOfferDto> RequestedCactuses { get; set; }
    }
}
