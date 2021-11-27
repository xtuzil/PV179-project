namespace BL.DTOs
{
    public class SpeciesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public GenusDto Genus { get; set; }

        public bool Approved { get; set; }
    }
}
