using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CactusCreateDto
    {
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public int SpeciesId { get; set; }

        public bool ForSale { get; set; }

        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        public int Amount { get; set; }

        public string Note { get; set; }
    }
}
