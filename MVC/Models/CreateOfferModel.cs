using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class CreateOfferModel
    {
        public OfferCreateDto Offer;
        public List<CactusDto> MyCollection;
        public List<CactusDto> YourCollection;
    }
}
