using System;

namespace Sat.Recruitment.Api.Exceptions
{
	[Serializable]
	public sealed class FileReadingException : ApplicationException
	{
		public FileReadingException() { }

		public FileReadingException(string message) : base(message) { }

		public FileReadingException(string message, Exception innerException) : base(message, innerException) { }
	}
}
