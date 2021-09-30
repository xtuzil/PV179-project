using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    // TODO: I guess we won't need this, it is enough to set whether a species is already accepted or not
    class NewSpeciesRequest : DatedEntity
    {
        public int AdvertiserId { get; set; }

        [ForeignKey(nameof(AdvertiserId))]
        public virtual User Advertiser { get; set; }

        public string SpeciesName { get; set; }

        public int GenusId { get; set; }

        [ForeignKey(nameof(GenusId))]
        public virtual Genus Genus { get; set; }
    }
}
