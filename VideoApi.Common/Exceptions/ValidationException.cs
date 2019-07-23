using System;

namespace VideoApi.Common.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(string message)
			:base(message)
		{ }

	}
}
