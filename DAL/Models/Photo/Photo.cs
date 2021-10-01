using System.Collections.Generic;

namespace CactusDAL.Models
{
    public class Photo : DatedEntity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }
}
