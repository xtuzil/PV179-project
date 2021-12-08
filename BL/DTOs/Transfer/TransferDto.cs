using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class TransferDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public OfferDto Offer { get; set; }

        public ReviewDto AuthorReview { get; set; }

        public ReviewDto RecipientReview { get; set; }

        public bool AuthorAprovedDelivery { get; set; }
        public bool RecipientAprovedDelivery { get; set; }

        public DateTime TransferedTime { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
