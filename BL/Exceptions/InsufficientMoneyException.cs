using System;

namespace BL.Exceptions
{
    public class InsufficientMoneyException : InvalidOperationException
    {
        public InsufficientMoneyException() : base(string.Format("Insufficient money on account(s).")) { }
    }
}
