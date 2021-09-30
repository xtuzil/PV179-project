using System.Collections.Generic;

namespace CactusDAL.Models
{
    public class Genus : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Species> Species { get; set; }
    }
}
