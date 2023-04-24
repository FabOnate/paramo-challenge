using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class FileReaderServiceTests
    {
		private readonly Mock<IUserService> _userService;
		private readonly UsersController _controller;

		public FileReaderServiceTests()
		{
			_userService = new Mock<IUserService>();
			_controller = new UsersController(_userService.Object);
		}

		[Fact]
		public async Task CreateUserService_Should_Be_Called()
		{
			// Arrange
			var user = new User { Email = "test.name@example.com", Money = 100 };

			// Act
			var result = await _controller.CreateUser(user);

			// Assert
			_userService.Verify(x => x.CreateUser(It.IsAny<User>()), Times.Once);
		}

	}
}
