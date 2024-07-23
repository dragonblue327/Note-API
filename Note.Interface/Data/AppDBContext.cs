using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface.Data
{
	public class AppDBContext :DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
		public DbSet<Domain.Entity.Note> Notes { get; set; }
	}
}
