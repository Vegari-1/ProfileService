using System;

namespace ProfileService.Service.Interface.Exceptions
{
	public class ApiException : BaseException
	{
		public ApiException(string apiName) : base(
			 String.Format("Service {0} not available", apiName))
		{
			StatusCode = 503;
		}
	}
}
