using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class BaseTransfer : BaseEntity
    {
        public int FromId { get; set; }
        public User From { get; set; }

        public int ToId { get; set; }
        public User To { get; set; }
    }
}
