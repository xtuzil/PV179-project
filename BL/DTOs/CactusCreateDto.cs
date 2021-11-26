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

        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        public int Amount { get; set; }

        public string Note { get; set; }
    }
}
