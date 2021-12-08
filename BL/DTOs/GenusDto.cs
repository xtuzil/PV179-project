using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class GenusDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
        public string Description { get; set; }
    }
}
