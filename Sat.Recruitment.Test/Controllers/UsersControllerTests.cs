using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class FileReaderServiceTests
    {
		private readonly Mock<IValidationService> _validationServiceMock;
		private readonly Mock<IFileReaderService> _fileReaderServiceMock;
		private readonly UsersController _controller;

		public FileReaderServiceTests()
		{
			_validationServiceMock = new Mock<IValidationService>();
			_fileReaderServiceMock = new Mock<IFileReaderService>();
			_controller = new UsersController(_validationServiceMock.Object, _fileReaderServiceMock.Object);
		}

		[Fact]
        public async Task CreateUser_WithValidUser_ReturnsSuccessResult()
        {
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };
			var users = new List<User> { new User { Email = "test2.name@example.com", Money = 50 } };
			_validationServiceMock.Setup(x => x.NormalizeEmail(user.Email)).Returns(user.Email);
			_fileReaderServiceMock.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(users);
			_validationServiceMock.Setup(x => x.CalculateMoneyPercentage(user)).Returns(120);

			// Act
			var result = await _controller.CreateUser(user);

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
			_validationServiceMock.Setup(x => x.NormalizeEmail(user.Email)).Returns(user.Email);
			_validationServiceMock.Setup(x => x.IsDuplicated(users, user)).Returns(true);
			_fileReaderServiceMock.Setup(x => x.ReadUsersFromFile()).ReturnsAsync(users);

			// Act
			var result = await _controller.CreateUser(user);

			// Assert
			Assert.False(result.IsSuccess);
			Assert.Equal("User already exists.", result.Message);
		}

		[Fact]
		public async Task CreateUser_WithException_ReturnsFailResult()
		{
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };
			_validationServiceMock.Setup(x => x.NormalizeEmail(user.Email)).Throws(new Exception("Error"));

			// Act
			var result = await _controller.CreateUser(user);

			// Assert
			Assert.False(result.IsSuccess);
			Assert.Equal("There was an error while creating the user: Error", result.Message);
		}
	}
}
