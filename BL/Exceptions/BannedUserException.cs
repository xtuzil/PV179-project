using System;

namespace BL.Exceptions
{
    public class BannedUserException : UnauthorizedAccessException
    {
        public BannedUserException() { }
        public BannedUserException(int userId) : base(string.Format("User with id {0} attempted to log in despite being banned.", userId)) { }
    }
}
