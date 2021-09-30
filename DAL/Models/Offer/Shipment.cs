using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Shipment
    {
        [Key]
        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; }

        public ShipmentStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public DateTime DateShipped { get; set; }
        public DateTime DateConfirmed { get; set; }
    }
}
