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

        public IEnumerable<Cactus> OfferedCactuses { get; set; }
        public IEnumerable<CactusOffered> CactusOffers { get; set; }
        public double? OfferedMoney { get; set; }

        public IEnumerable<Cactus> RequestedCactuses { get; set; }
        public IEnumerable<CactusRequested> CactusRequests { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferResponse Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public int? PreviousOfferId { get; set; }
        [ForeignKey(nameof(PreviousOfferId))]
        public Offer PreviousOffer { get; set; }

        public Offer NextOffer { get; set; }

        public Shipment Shipment { get; set; }
    }
}
