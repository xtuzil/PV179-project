﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class TransferDto
    {
        public int Id { get; set; }
        public OfferDto Offer { get; set; }

        public ReviewDto AuthorReview { get; set; }

        public ReviewDto RecipientReview { get; set; }

        public bool AuthorAprovedDelivery { get; set; }
        public bool RecipientAprovedDelivery { get; set; }

        public DateTime TransferedTime;
        public DateTime CreationDate { get; set; }
    }
}