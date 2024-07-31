using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Note.Application.Common.Behaviours;
using System.Reflection;

namespace Note.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(ctg =>
			{
				ctg.AddBehavior(typeof(IPipelineBehavior<,> ), typeof(ValidationBehaviour<,>));
				ctg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
			});
			return services;
		}
	}
}
