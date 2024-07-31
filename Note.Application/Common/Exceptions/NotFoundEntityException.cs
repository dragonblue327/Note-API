using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Common.Exceptions
{

	public class NotFoundEntityException : Exception
	{
		public NotFoundEntityException()
		{
		}

		public NotFoundEntityException(string message)
			: base(message)
		{
		}

		public NotFoundEntityException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

}
