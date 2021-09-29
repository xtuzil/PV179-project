using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class BaseOffer : DatedEntity
    {
        public int AdvertiserId { get; set; }

        [ForeignKey(nameof(AdvertiserId))]
        public User Advertiser { get; set; }

        public int BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public User Buyer { get; set; }

        public bool Sold { get; set; }

    }
}
