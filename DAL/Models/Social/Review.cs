using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Review : DatedEntity
    {
        public string Text { get; set; }
        public double Score { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public int TransferId { get; set; }

        [ForeignKey(nameof(TransferId))]
        public virtual Transfer Transfer { get; set; }
    }
}
