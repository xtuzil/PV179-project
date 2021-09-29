using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Report : DatedEntity
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public int ReasonId { get; set; }
        [ForeignKey(nameof(ReasonId))]
        public ReportReason Reason { get; set; }
    }
}
