using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using Sat.Recruitment.Api.Exceptions;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IValidationService _validationService;
		private readonly IFileReaderService _fileReaderService;

		public UsersController(
			IValidationService validationService,
			IFileReaderService fileReaderService)
        {
			_validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
			_fileReaderService = fileReaderService ?? throw new ArgumentNullException(nameof(fileReaderService));
		}


		/// <summary>
		///     Create User endpoint
		/// </summary>
		/// <param name="user">user propeties</param>
		[HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser([FromBody] User user)
        {
            try
            {
				user.Email = _validationService.NormalizeEmail(user.Email);
				var users = await _fileReaderService.ReadUsersFromFile();

				user.Money = _validationService.CalculateMoneyPercentage(user);

				if (_validationService.IsDuplicated(users, user))
                {
					return Result.Fail("User already exists.");
                }

				_fileReaderService.WriteFile(user);
				return Result.Success("User Created.");
			}
			catch (FileReadingException ex)
			{
				return Result.Fail(ex.Message);
			}
			catch (Exception ex)
            {
				return Result.Fail($"There was an error while creating the user: {ex.Message}");
			}

        }

        
    }
}
