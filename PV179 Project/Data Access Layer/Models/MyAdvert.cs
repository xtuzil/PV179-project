using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class MyAdvert : DatedEntity
    {
        public int AdvertiserId { get; set; }
        public User Advertiser { get; set; }

        public IEnumerable<Cactus> OfferedCactuses { get; set; }
        //public double? OfferedMoney { get; set; }

        //public IEnumerable<Species> RequestedSpecies { get; set; }
        //public double? RequestedMoney { get; set; }
    }
}
