using System;

namespace ProfileService.Service.Interface.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(Type entityType, string entityField) : base(
             String.Format("{0} with given {1} value is not valid", entityType.Name, entityField))
        {
            StatusCode = 400;
        }
    }
}
