using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class Transfer : BaseEntity
    {
        public int FromId { get; set; }
        [ForeignKey(nameof(FromId))]
        public User From { get; set; }

        public int ToId { get; set; }
        [ForeignKey(nameof(ToId))]
        public User To { get; set; }

        public int? CactusId { get; set; }
        [ForeignKey(nameof(CactusId))]
        public Cactus Cactus { get; set; }

        public double? Amount { get; set; }
    }
}
