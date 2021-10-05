﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class CactusOffer: BaseEntity
    {
        public int CactusId { get; set; }
        [ForeignKey(nameof(CactusId))]
        public Cactus Cactus { get; set; }
        public int Amount { get; set; }

        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; }
    }
}