using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CactusRequested
    {
        public int CactusId { get; set; }
        [ForeignKey(nameof(CactusId))]
        public Cactus Cactus { get; set; }

        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public MyBaseOffer Offer { get; set; }
    }
}
