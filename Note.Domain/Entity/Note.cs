using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Domain.Entity
{
	public class Note
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public List<Tag> Tags { get; set; }
	}

}
