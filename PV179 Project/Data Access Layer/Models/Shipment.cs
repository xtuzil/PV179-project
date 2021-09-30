using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Shipment
    {
        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public MyBaseOffer Offer { get; set; }

        public ShipmentStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public DateTime DateShipped { get; set; }
        public DateTime DateConfirmed { get; set; }
    }
}
