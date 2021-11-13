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
        public UserInfoDto Owner { get; set; }

        public SpeciesDto Species { get; set; }

        public bool ForSale { get; set; }

        public DateTime SowingDate { get; set; }
        public int PotSize { get; set; }
        public int Amount { get; set; }

        public string Note { get; set; }
    }
}
