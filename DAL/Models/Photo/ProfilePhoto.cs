using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class ProfilePhoto : Photo
    {
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
