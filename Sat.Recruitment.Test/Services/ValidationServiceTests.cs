using Moq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interface;
using Sat.Recruitment.Api.Services.Validators;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("ValidationTests", DisableParallelization = true)]
    public class ValidationServiceTests
	{
		private readonly IValidationService _validationService;
		private readonly Mock<IUserDuplicationValidator> _userDuplicationValidator;
		private readonly Mock<IEmailNormalizer> _emailNormalizer;
		private readonly Mock<IMoneyPercentageCalculator> _moneyPercentageCalculator;


		public ValidationServiceTests()
		{
			_userDuplicationValidator = new Mock<IUserDuplicationValidator>();
			_emailNormalizer = new Mock<IEmailNormalizer>();
			_moneyPercentageCalculator = new Mock<IMoneyPercentageCalculator>();
			_validationService = new ValidationService(_userDuplicationValidator.Object, _emailNormalizer.Object, _moneyPercentageCalculator.Object);
		}

		[Fact]
		public void EmailNormalizer_Should_Be_Called()
		{
			// Arrange
			var user = new User { Email = "test@test.com" };

			// Act
			var result = _validationService.NormalizeEmail(user.Email);

			// Assert
			_emailNormalizer.Verify(x => x.NormalizeEmail(It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public void MoneyPercentageCalculator_Should_Be_Called()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "Normal" };

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			_moneyPercentageCalculator.Verify(x => x.CalculateMoneyPercentage(It.IsAny<User>()), Times.Once);
		}

		[Fact]
		public void UserDuplicationValidator_Should_Be_Called()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "Normal", Email = "test@test.com" };
			List<User> users = new List<User>();

			// Act
			var result = _validationService.IsDuplicated(users, user);

			// Assert
			_userDuplicationValidator.Verify(x => x.IsDuplicated(It.IsAny<List<User>>(), It.IsAny<User>()), Times.Once);
		}

	}
}
