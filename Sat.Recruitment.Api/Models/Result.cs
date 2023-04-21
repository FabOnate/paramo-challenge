namespace Sat.Recruitment.Api.Models
{
	public class Result
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public static Result Success(string sucessMessage)
		{
			return new Result { IsSuccess = true, Message = sucessMessage };
		}

		public static Result Fail(string errorMessage)
		{
			return new Result { IsSuccess = false, Message = errorMessage };
		}
	}
}
