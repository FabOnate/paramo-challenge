using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interface
{
    public interface IUserService
	{
		Task<Result> CreateUser(User user);

		Task<IList<User>> GetUsers();

	}
}
