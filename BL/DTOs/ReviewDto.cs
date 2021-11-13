using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ReviewDto
    {
        public string Text { get; set; }
        public double Score { get; set; }
        public UserInfoDto User { get; set; }
        public UserInfoDto Author { get; set; }

        public TransferDto Transfer { get; set; }
    }
}
