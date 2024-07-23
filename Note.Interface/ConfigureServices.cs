using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices
			(this IServiceCollection services , IConfiguration configration) {
			return services;
		}
	}
}
