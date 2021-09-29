using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    public class CactusPhoto : Photo
    {
        public int UploaderId { get; set; }

        [ForeignKey(nameof(UploaderId))]
        public User Uploader { get; set; }

        public int CactusId { get; set; }

        [ForeignKey(nameof(CactusId))]
        public Cactus Cactus { get; set; }
    }
}
