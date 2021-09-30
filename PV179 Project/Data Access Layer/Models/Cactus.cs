using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
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
        public IEnumerable<CactusRequested> Requests { get; set; }
        public IEnumerable<MyBaseOffer> RequestedIn { get; set; }
        //public IEnumerable<CactusRequested> RequestedIn { get; set; }
        public IEnumerable<CactusOffered> Offers { get; set; }
        public IEnumerable<MyBaseOffer> OfferedIn { get; set; }
        //public IEnumerable<CactusOffered> OfferedIn { get; set; }

        public IEnumerable<Transfer> Transfers { get; set; }

        public bool ForSale { get; set; }

        // TODO: any additional details that could be optionally stored?

        public string Note { get; set; }
        // TODO: separate entity maybe to be able to add dated entries
        // and log some events in the life of the cactus?
    }
}
