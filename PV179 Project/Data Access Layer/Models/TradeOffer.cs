using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class TradeOffer : BaseOffer
    {

        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }

    }
}
