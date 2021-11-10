using CactusDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CactusDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public int SpeciesId { get; set; }

        public bool ForSale { get; set; }

        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        public int Amount { get; set; }

        public string Note { get; set; }
    }
}
