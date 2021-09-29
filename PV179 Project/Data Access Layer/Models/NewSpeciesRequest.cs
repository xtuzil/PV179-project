using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
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
