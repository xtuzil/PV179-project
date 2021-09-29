namespace Data_Access_Layer.Models
{
    public class PostalAddress : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; } // TODO: enum or separate entity perhaps?
    }
}
