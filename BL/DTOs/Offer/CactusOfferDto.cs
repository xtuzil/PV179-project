namespace BL.DTOs
{
    public class CactusOfferDto
    {
        public int Id { get; set; }
        public CactusDto Cactus { get; set; }
        public int Amount { get; set; }
        public OfferDto Offer { get; set; }
    }
}
