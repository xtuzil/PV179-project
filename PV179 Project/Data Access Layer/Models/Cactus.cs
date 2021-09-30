using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Cactus : DatedEntity
    {
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public int SpeciesId { get; set; }

        [ForeignKey(nameof(SpeciesId))]
        public Species Species { get; set; }

        public IEnumerable<CactusPhoto> Photos { get; set; }

        public bool ForSale { get; set; }

        // TODO: any additional details that could be optionally stored?

        public string Note { get; set; }
        // TODO: separate entity maybe to be able to add dated entries
        // and log some events in the life of the cactus?
    }
}
