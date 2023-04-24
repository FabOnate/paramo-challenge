using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.Validators
{
    public interface IUserDuplicationValidator
	{
		bool IsDuplicated(IList<User> users, User user);
	}
}
