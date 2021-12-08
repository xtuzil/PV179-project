using System;
using System.ComponentModel.DataAnnotations;

namespace BL.DTOs
{
    public class ReportDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public UserInfoDto Target { get; set; }
        [Required]
        public UserInfoDto Author { get; set; }
        public string Description;
        public DateTime CreationDate { get; set; }
    }
}
