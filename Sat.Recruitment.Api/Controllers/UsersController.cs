using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

		public UsersController(
			IUserService userService)
        {
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
		}


        /// <summary>
        ///     Create User endpoint
        /// </summary>
        /// <param name="user">user propeties</param>
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser([FromBody] User user)
        {
            return await _userService.CreateUser(user);
        }
        
    }
}
