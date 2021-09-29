using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    class Comment : DatedEntity
    {
        public string Text { get; set; }
        public User AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
    }
}
