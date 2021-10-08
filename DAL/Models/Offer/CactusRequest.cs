using CactusDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class CactusRequest : BaseEntity
    {
        public int CactusId { get; set; }
        [ForeignKey(nameof(CactusId))]
        public virtual Cactus Cactus { get; set; }
        public int Amount { get; set; }

        public int OfferId { get; set; }
        [ForeignKey(nameof(OfferId))]
        public virtual Offer Offer { get; set; }
    }
}
