using BL.DTOs;
using System.Collections.Generic;

namespace MVC.Models
{
    public class CreateOfferModel
    {
        public OfferCreateDto Offer;
        public List<CactusDto> MyCollection;
        public List<CactusDto> YourCollection;
    }
}
