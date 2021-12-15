using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class InsufficientMoneyException:InvalidOperationException
    {
        public InsufficientMoneyException() : base(string.Format("Insufficient money on account(s).")) { }
    }
}
