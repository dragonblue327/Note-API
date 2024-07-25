using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
	public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
	{
		var exception = context.Exception;
		var result = new ObjectResult(new { error = exception.Message })
		{
			StatusCode = 500
		};
		context.Result = result;
		context.ExceptionHandled = true;
	}
}
