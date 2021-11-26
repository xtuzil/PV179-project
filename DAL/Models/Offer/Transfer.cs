using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Transfer : DatedEntity
    {
        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }

        public int? AuthorReviewId { get; set; }
        [ForeignKey(nameof(AuthorReviewId))]
        public virtual Review AuthorReview { get; set; }

        public int? RecipientReviewId { get; set; }
        [ForeignKey(nameof(RecipientReviewId))]
        public virtual Review RecipientReview { get; set; }

        public bool AuthorAprovedDelivery { get; set; }
        public bool RecipientAprovedDelivery { get; set; }

        public DateTime TransferedTime { get; set; }




    }
}
