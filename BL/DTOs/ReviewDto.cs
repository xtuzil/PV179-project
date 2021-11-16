using System;

namespace BL.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public double Score { get; set; }
        public UserInfoDto User { get; set; }
        public UserInfoDto Author { get; set; }

        public TransferDto Transfer { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
