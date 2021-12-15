using BL.Enums;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class SpeciesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public GenusDto Genus { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }
    }
}
