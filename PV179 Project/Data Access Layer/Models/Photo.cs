namespace Data_Access_Layer.Models
{
    public class Photo : DatedEntity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
    }
}
