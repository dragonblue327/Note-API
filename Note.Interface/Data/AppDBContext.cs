using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface.Data
{
	public class AppDBContext : DbContext
	{
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Reminder> Reminders { get; set; }
		public DbSet<Domain.Entity.Note> Notes { get; set; }
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

	}
}
