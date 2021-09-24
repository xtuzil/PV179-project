using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Species
    {
        public int Id { get; set; }
        public Genus Genus { get; set; }
        public IEnumerable<Cactus> Cactuses { get; set; }
    }
}
