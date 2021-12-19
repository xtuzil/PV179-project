using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class TransferUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OfferId { get; set; }
        public bool AuthorAprovedDelivery { get; set; }
        public bool RecipientAprovedDelivery { get; set; }

        public DateTime TransferedTime { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
