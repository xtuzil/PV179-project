using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class MyBaseOffer : DatedEntity
    {
        public int SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        public IEnumerable<Cactus> OfferedCactuses { get; set; }
        public IEnumerable<CactusOffered> Offers { get; set; }
        //public IEnumerable<CactusOffered> OfferedCactuses { get; set; }
        public double? OfferedMoney { get; set; }

        public IEnumerable<Cactus> RequestedCactuses { get; set; }
        public IEnumerable<CactusRequested> Requests { get; set; }
        //public IEnumerable<CactusRequested> RequestedCactuses { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferResponse Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public MyBaseOffer NextOffer { get; set; }

        public int? PreviousOfferId { get; set; }
        [ForeignKey(nameof(PreviousOfferId))]
        public MyBaseOffer PreviousOffer { get; set; }

        public Shipment Shipment { get; set; }
    }
}
