using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Review : DatedEntity
    {
        public string Text { get; set; }
        public double Score { get; set; }

        public User SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public virtual User Seller { get; set; }

        public User AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
    }
}
