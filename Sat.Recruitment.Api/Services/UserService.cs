using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {

		private readonly IFileService _fileReaderService;
		private readonly IValidationService _validationService;

		public UserService(IFileService fileReaderService,
			IValidationService validationService)
		{
			_fileReaderService = fileReaderService ?? throw new ArgumentNullException(nameof(fileReaderService)); ;
			_validationService = validationService ?? throw new ArgumentNullException(nameof(validationService)); ;
		}

		/// <summary>
		///    Create User
		/// </summary>
		/// <param name="user">user object</param>
		public async Task<Result> CreateUser(User user)
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

		public async Task<IList<User>> GetUsers()
		{
			return await _fileReaderService.ReadUsersFromFile();
		}

	}
}
