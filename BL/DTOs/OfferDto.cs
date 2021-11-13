using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public enum OfferStatus
    {
        Accepted,
        Declined,
        Counteroffer,
        Transfered
    }

    public class OfferDto
    {
        public UserInfoDto Author { get; set; }

        public UserInfoDto Recipient { get; set; }
        public double? OfferedMoney { get; set; }
        public double? RequestedMoney { get; set; }

        public OfferStatus Response { get; set; }
        public DateTime ResponseDate { get; set; }

        public OfferDto PreviousOffer { get; set; }
    }
}
