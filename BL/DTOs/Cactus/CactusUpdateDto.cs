using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class CactusUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        [Required]
        public SpeciesDto Species { get; set; }
        public bool ForSale { get; set; }
        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        [Range(1, 1000000, ErrorMessage = "{0} should be minimum of {1}.")]
        [Required]
        public int Amount { get; set; }
        [StringLength(1000, ErrorMessage = "The {0} can not exceed {1} characters long.")]
        public string Note { get; set; }
        public byte[] Image { get; set; }
    }
}
