using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Offer : DatedEntity
    {
        public int SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        public IEnumerable<CactusOffer> CactusOffers { get; set; }
        public double? OfferedMoney { get; set; }

        public IEnumerable<CactusOffer> CactusRequests { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public int? PreviousOfferId { get; set; }
        [ForeignKey(nameof(PreviousOfferId))]
        public Offer PreviousOffer { get; set; }


    }
}
