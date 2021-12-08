using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class ReviewDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(200, ErrorMessage = "{0} length can't be more than {1}.")]
        public string Text { get; set; }
        [Range(0.00, 5.00, ErrorMessage = "{0} can be between {1} and {2}.")]
        public double Score { get; set; }
        [Required]
        public UserInfoDto User { get; set; }
        [Required]
        public UserInfoDto Author { get; set; }
        [Required]
        public TransferDto Transfer { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
