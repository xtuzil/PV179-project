using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Offer : BaseOffer
    {
        public IEnumerable<TradeOffer> TradeOffers { get; set; }

        public int CactusId { get; set; }

        [ForeignKey(nameof(CactusId))]
        public virtual Cactus Cactus { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }

        public int ReviewId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public virtual Review Review { get; set; }

    }
}
