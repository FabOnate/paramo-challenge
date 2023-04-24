using Moq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("ValidationTests", DisableParallelization = true)]
    public class UserServiceTests
	{
		private readonly Mock<IFileService> _fileReaderService;
		private readonly Mock<IValidationService> _validationService;
		private readonly IUserService _userService;

		public UserServiceTests()
		{
			_fileReaderService = new Mock<IFileService>();
			_validationService = new Mock<IValidationService>();
			_userService = new UserService(_fileReaderService.Object, _validationService.Object);
		}

		[Fact]
		public async Task CreateUser_WithValidUser_ReturnsSuccessResult()
		{
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };
			var users = new List<User> { new User { Email = "test2.name@example.com", Money = 50 } };
			_validationService.Setup(x => x.NormalizeEmail(user.Email)).Returns(user.Email);
			_fileReaderService.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(users);
			_validationService.Setup(x => x.CalculateMoneyPercentage(user)).Returns(120);

			// Act
			var result = await _userService.CreateUser(user);

			// Assert
			Assert.True(result.IsSuccess);
			Assert.Equal("User Created.", result.Message);
		}

		[Fact]
		public async Task CreateUser_WithDuplicateUser_ReturnsFailResult()
		{
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };
			var users = new List<User> { new User { Email = "test.name@example.com", Money = 50 } };
			_validationService.Setup(x => x.NormalizeEmail(user.Email)).Returns(user.Email);
			_validationService.Setup(x => x.IsDuplicated(users, user)).Returns(true);
			_fileReaderService.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(users);

			// Act
			var result = await _userService.CreateUser(user);

			// Assert
			Assert.False(result.IsSuccess);
			Assert.Equal("User already exists.", result.Message);
		}

		[Fact]
		public async Task CreateUser_WithException_ReturnsFailResult()
		{
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };
			_validationService.Setup(x => x.NormalizeEmail(user.Email)).Throws(new Exception("Error"));

			// Act
			var result = await _userService.CreateUser(user);

			// Assert
			Assert.False(result.IsSuccess);
			Assert.Equal("There was an error while creating the user: Error", result.Message);
		}

	}
}
