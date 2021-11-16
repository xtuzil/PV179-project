using System;

namespace BL.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public UserInfoDto Target { get; set; }
        public UserInfoDto Author { get; set; }
        public string Description;
        public DateTime CreationDate { get; set; }
    }
}
