using System;

namespace ProfileService.Service.Interface.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() : base(
             "Forbidden action")
        {
            StatusCode = 403;
        }
    }
}
