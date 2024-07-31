using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Note.Domain.Repository;
using Note.Infrastructure.Data;
using Note.Infrastructure.Repository;

namespace Note.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices
		(this IServiceCollection services, IConfiguration configration)
	{
		services.AddDbContext<AppDBContext>(options =>
		options.UseSqlServer(configration.GetConnectionString("ConStr") ??
		throw new InvalidOperationException("Connection String 'ConStr' Not Found")));
		
		services.AddScoped<CustomExceptionFilter>();
		services.AddTransient<INoteRepository, NoteRepository>();
		services.AddTransient<IReminderRepository, ReminderRepository>();
		services.AddTransient<ITagRepository, TagRepository>();
		
		return services;
	}
}
