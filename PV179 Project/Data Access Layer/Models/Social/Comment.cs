using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    class Comment : DatedEntity
    {
        public string Text { get; set; }
        public User AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
    }
}
