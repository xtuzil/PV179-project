using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Genus : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Species> Species { get; set; }
    }
}
