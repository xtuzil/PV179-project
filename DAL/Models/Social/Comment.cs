using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    // @ optional feature
    public class Comment : DatedEntity
    {
        public string Text { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }



    }
}
