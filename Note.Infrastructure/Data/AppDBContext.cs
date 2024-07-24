using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;


namespace Note.Infrastructure.Data
{
	public class AppDBContext : DbContext
	{
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Reminder> Reminders { get; set; }
		public DbSet<Domain.Entity.Note> Notes { get; set; }
		
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Domain.Entity.Note>()
				.HasMany(n => n.Tags)
				.WithMany(t => t.Notes)
				.UsingEntity(j => j.ToTable("NoteTag"));

			modelBuilder.Entity<Reminder>()
				.HasMany(r => r.Tags)
				.WithMany(t => t.Reminders)
				.UsingEntity(j => j.ToTable("ReminderTag"));
		}

	}
}

