using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VideoApi.Common.Exceptions;

namespace VideoApi.Web
{
	public class ExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			if (context.Exception is ValidationException validationException)
			{
				context.Result = new BadRequestObjectResult(new
				{
					error = validationException.Message
				});
			}
			else
			{
				context.Result = new StatusCodeResult(500);
			}
		}
	}
}
