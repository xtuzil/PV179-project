using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Offer : DatedEntity
    {
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public int RecipientId { get; set; }
        [ForeignKey(nameof(RecipientId))]
        public virtual User Recipient { get; set; }

        public virtual IEnumerable<CactusOffer> CactusOffers { get; set; }
        public double? OfferedMoney { get; set; }

        public virtual IEnumerable<CactusOffer> CactusRequests { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public int? PreviousOfferId { get; set; }
        [ForeignKey(nameof(PreviousOfferId))]
        public virtual Offer PreviousOffer { get; set; }


    }
}
