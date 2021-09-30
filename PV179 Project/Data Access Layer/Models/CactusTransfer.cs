using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CactusTransfer : BaseTransfer
    {
        public int CactusId { get; set; }
        public Cactus Cactus { get; set; }
    }
}
