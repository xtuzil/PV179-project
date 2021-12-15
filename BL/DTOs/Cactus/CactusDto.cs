using System;
using System.ComponentModel.DataAnnotations;

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
