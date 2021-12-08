using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class SpeciesCreateDto
    {
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        [Required]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
        public string LatinName { get; set; }
        [Required]
        public int GenusId { get; set; }
    }
}
