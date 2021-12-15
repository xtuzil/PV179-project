using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
