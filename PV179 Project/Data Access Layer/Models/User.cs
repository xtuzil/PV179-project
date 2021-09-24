using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class User
    {
        public int Id { get; set; }
        public IEnumerable<Cactus> Cactuses { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<CounterOffer> CounterOffers { get; set; }
    }
}
