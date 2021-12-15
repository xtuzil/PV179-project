
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class CactusOfferDto
    {
        public int Id { get; set; }
        public virtual CactusDto Cactus { get; set; }
        public int CactusId { get; set; }
        public int Amount { get; set; }
        public virtual OfferDto Offer { get; set; }
    }
}
