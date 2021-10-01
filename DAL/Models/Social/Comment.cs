using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Comment : DatedEntity
    {
        public string Text { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
    }
}
