using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interface
{
    public interface IFileReaderService
    {
		Task<IList<User>> ReadUsersFromFile();
		void WriteFile(User user);


	}
}
