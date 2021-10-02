using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Transfer : DatedEntity
    {
        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; }

        public int SenderReviewId { get; set; }
        [ForeignKey(nameof(SenderReviewId))]
        public Review SenderReview { get; set; }

        public int ReceiverReviewId { get; set; }
        [ForeignKey(nameof(ReceiverReviewId))]
        public Review ReceiverReview { get; set; }

        public bool SenderReceivedAprovedDelivery { get; set; }
        public bool ReceiverAprovedDelivery { get; set; }

        public DateTime TransferedTime;




    }
}
