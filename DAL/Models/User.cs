using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ProfilePhoto ProfilePhoto { get; set; }

        public Role Role { get; set; }

        // TODO: banned indefinitely?
        // separate bool 'BannedForever' or DateTime.MaxValue?
        public DateTime? BannedUntil { get; set; }

        public double AccountBalance { get; set; }

        public string PhoneNumber { get; set; }

        // TODO: be able to store multiple addresses and set one primary perhaps?
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public PostalAddress Address { get; set; }

        public IEnumerable<Cactus> Cactuses { get; set; }
        public IEnumerable<Species> Wishlist { get; set; }

        public IEnumerable<Offer> OffersSent { get; set; }
        public IEnumerable<Offer> OffersReceived { get; set; }

        public IEnumerable<Species> SpeciesSuggested { get; set; }
        public IEnumerable<Species> SpeciesConfirmed { get; set; }

        public IEnumerable<Report> ReportsSent { get; set; }
        public IEnumerable<Report> ReportsReceived { get; set; }

        public IEnumerable<Review> ReviewsSent { get; set; }
        public IEnumerable<Review> ReviewsReceived { get; set; }

        public IEnumerable<Transfer> TransfersFrom { get; set; }
        public IEnumerable<Transfer> TransfersTo { get; set; }
    }
}
