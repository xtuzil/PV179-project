using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class CactusOffer : BaseEntity
    {
        public int CactusId { get; set; }
        [ForeignKey(nameof(CactusId))]
        public virtual Cactus Cactus { get; set; }
        public int Amount { get; set; }

        public int RequestedId { get; set; }
        [ForeignKey(nameof(RequestedId))]
        public virtual Offer Requested { get; set; }

        public int OfferedId { get; set; }
        [ForeignKey(nameof(OfferedId))]
        public virtual Offer Offered { get; set; }
    }
}
