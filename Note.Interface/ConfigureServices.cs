using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Note.Domain.Repository;
using Note.Interface.Data;
using Note.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices
		(this IServiceCollection services, IConfiguration configration)
	{
		services.AddDbContext<AppDBContext>(options =>
		options.UseSqlServer(configration.GetConnectionString("ConStr") ??
		throw new InvalidOperationException("Connection String 'ConStr' Not Found")));

		services.AddTransient<INoteRepository, NoteRepository>();

		return services;
	}
}
