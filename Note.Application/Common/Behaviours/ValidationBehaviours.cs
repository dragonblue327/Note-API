using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Common.Behaviours
{
	public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			this._validators = validators;
		}
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResult = await Task.WhenAll(_validators
										.Select(a => a.ValidateAsync(context, cancellationToken)));
				var failures = validationResult.Where(a => a.Errors.Any())
								.SelectMany(a => a.Errors).ToList();
				if (failures.Any())
				{
					throw new ValidationException(failures);
				}
			}
			return await next();
		}
	}
}
