using System.ComponentModel.DataAnnotations.Schema;

namespace CactusDAL.Models
{
    public class CactusPhoto : Photo
    {
        public int CactusId { get; set; }

        [ForeignKey(nameof(CactusId))]
        public virtual Cactus Cactus { get; set; }
    }
}
