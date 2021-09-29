using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int ProfilePhotoId { get; set; }
        [ForeignKey(nameof(ProfilePhotoId))]
        public ProfilePhoto ProfilePhoto { get; set; }

        public Role Role { get; set; }

        // TODO: banned indefinitely?
        // separate bool 'BannedForever' or DateTime.MaxValue?
        public DateTime BannedUntil { get; set; }

        // TODO: account balance? or should we always calculate it from the history of transactions?

        public string PhoneNumber { get; set; }

        // TODO: be able to store multiple addresses and set one primary perhaps?
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public PostalAddress Address { get; set; }

        public IEnumerable<Cactus> Cactuses { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<TradeOffer> TradeOffers { get; set; }
        public IEnumerable<Review> Review { get; set; }
        public IEnumerable<Report> Reports { get; set; }
    }
}
