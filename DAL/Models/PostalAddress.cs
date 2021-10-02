namespace CactusDAL.Models
{
    public class PostalAddress : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
