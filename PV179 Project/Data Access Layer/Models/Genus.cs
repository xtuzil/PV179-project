using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Genus : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Species> Species { get; set; }
    }
}
