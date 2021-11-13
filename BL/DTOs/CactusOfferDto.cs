using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CactusOfferDto
    {
        public CactusDto Cactus { get; set; }
        public int Amount { get; set; }

        public OfferDto Offer { get; set; }
    }
}
