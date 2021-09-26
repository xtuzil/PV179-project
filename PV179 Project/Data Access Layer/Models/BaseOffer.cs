using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class BaseOffer : BaseEntity
    {
        public int AdvertiserId { get; set; }

        [ForeignKey(nameof(AdvertiserId))]
        public User Advertiser { get; set; }

        public int BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public User Buyer { get; set; }

        public DateTime CreationDate { get; set; }
        public bool Sold { get; set; }
        
    }
}
