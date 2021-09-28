namespace Data_Access_Layer.Models
{
    public class Cactus
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public Species Species { get; set; }
    }
}
