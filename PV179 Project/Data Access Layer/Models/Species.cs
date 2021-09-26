using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Species : BaseEntity
    {

        public string Name { get; set; }
        public int GenusId { get; set; }

        [ForeignKey(nameof(GenusId))]
        public virtual Genus Genus {get; set; }

        public IEnumerable<Cactus> Cactuses { get; set; }
    }
}
