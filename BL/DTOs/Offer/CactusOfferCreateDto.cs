using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CactusOfferCreateDto
    {
        public virtual int CactusId { get; set; }
        public int Amount { get; set; }

        public virtual int OfferId { get; set; }
    }
}
