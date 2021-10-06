using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Report : DatedEntity
    {
        public int TargetId { get; set; }
        [ForeignKey(nameof(TargetId))]
        public virtual User Target { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public string Description;
    }
}
