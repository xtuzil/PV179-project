using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CounterOffer
    {
        public int Id { get; set; }
        public Offer Offer { get; set; }
        public User Advertiser { get; set; }
    }
}
