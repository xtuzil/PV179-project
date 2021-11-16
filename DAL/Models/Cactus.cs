using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Cactus : DatedEntity
    {
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; }

        public int SpeciesId { get; set; }

        [ForeignKey(nameof(SpeciesId))]
        public virtual Species Species { get; set; }

        public virtual IEnumerable<CactusPhoto> Photos { get; set; }

        public virtual IEnumerable<Transfer> Transfers { get; set; }

        public bool ForSale { get; set; }

        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        public int Amount { get; set; }

        public string Note { get; set; }
        // TODO: separate entity maybe to be able to add dated entries
        // and log some events in the life of the cactus?
    }
}
