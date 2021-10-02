using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Report : DatedEntity
    {
        public int TargetId { get; set; }
        [ForeignKey(nameof(TargetId))]
        public User Target { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public string Description;
    }
}
