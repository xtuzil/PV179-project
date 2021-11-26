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

        public virtual ProfilePhoto ProfilePhoto { get; set; }
        public virtual IEnumerable<CactusPhoto> UploadedCactusPhotos { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public Role Role { get; set; }
        public bool Banned { get; set; }

        public double AccountBalance { get; set; }

        public string PhoneNumber { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual PostalAddress Address { get; set; }

        public virtual IEnumerable<Cactus> Cactuses { get; set; }
        public virtual IEnumerable<Wishlist> Wishlist { get; set; }

        public virtual IEnumerable<Offer> OffersSent { get; set; }
        public virtual IEnumerable<Offer> OffersReceived { get; set; }

        public virtual IEnumerable<Review> ReviewsReceived { get; set; }

        public virtual IEnumerable<Transfer> TransfersFrom { get; set; }
        public virtual IEnumerable<Transfer> TransfersTo { get; set; }
    }
}
