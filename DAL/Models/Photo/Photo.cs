namespace CactusDAL.Models
{
    public class Photo : DatedEntity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
    }
}
