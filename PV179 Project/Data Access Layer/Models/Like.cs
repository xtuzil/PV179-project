using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class Like : BaseEntity
    {
        public int PhotoId { get; set; }

        [ForeignKey(nameof(PhotoId))]
        public Photo Photo { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
