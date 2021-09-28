using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public string PhoneNumber { get; set; }


        // should be class alone
        public string Address { get; set; }
        public IEnumerable<Cactus> Cactuses { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<TradeOffer> TradeOffers { get; set; }
        public IEnumerable<Review> Review { get; set; }
    }
}
