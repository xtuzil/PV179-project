using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Wishlist : BaseEntity
    {
        public int SpeciesId { get; set; }

        [ForeignKey(nameof(SpeciesId))]
        public virtual Species Species { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
