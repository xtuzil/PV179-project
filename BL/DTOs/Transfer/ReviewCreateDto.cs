using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class ReviewCreateDto
    {
        [StringLength(200, ErrorMessage = "{0} length can't be more than {1}.")]
        public string Text { get; set; }
        [Range(0.00, 5.00, ErrorMessage = "{0} can be between {1} and {2}.")]
        public double Score { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int TransferId { get; set; }
    }
}
