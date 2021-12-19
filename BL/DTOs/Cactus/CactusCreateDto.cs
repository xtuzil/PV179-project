using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class CactusCreateDto
    {
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public int GenusId { get; set; }
        [Required]
        public int SpeciesId { get; set; }

        public bool ForSale { get; set; }

        [DataType(DataType.Date)]
        public DateTime SowingDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Pot size should be at least {1}.")]
        public int PotSize { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be at least {1}.")]
        [Required]
        public int Amount { get; set; }
        [StringLength(1000, ErrorMessage = "The length of the note cannot exceed {1} characters.")]
        public string Note { get; set; }
        public byte[] Image { get; set; }
    }
}
