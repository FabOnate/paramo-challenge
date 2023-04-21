using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interface;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("ValidationTests", DisableParallelization = true)]
    public class ValidationServiceTests
	{
		private readonly IValidationService _validationService;

		public ValidationServiceTests()
		{
			_validationService = new ValidationService();
		}

		[Theory]
		[InlineData("user@example.com", "user@example.com")]
		[InlineData("user.email@example.com", "useremail@example.com")]
		[InlineData("user.email+ @example.com", "useremail@example.com")]
		public void NormalizeEmail_ShouldNormalizeEmail(string originalEmail, string expectedEmail)
		{
			// Arrange

			// Act
			var normalizedEmail = _validationService.NormalizeEmail(originalEmail);

			// Assert
			Assert.Equal(expectedEmail, normalizedEmail);
		}

		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsNormalAndMoneyGreaterThan100_ShouldReturnMoneyIncreasedBy12Percent()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "Normal" };
			var expectedMoney = 224m;

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}

		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsNormalAndMoneyBetween10And100_ShouldReturnMoneyIncreasedBy8Percent()
		{
			// Arrange
			var user = new User { Money = 50, UserType = "Normal" };
			var expectedMoney = 54m;

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}

		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsSuperUserAndMoneyGreaterThan100_ShouldReturnMoneyIncreasedBy20Percent()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "SuperUser" };
			var expectedMoney = 240m;

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}

		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsPremiumAndMoneyGreaterThan100_ShouldReturnMoneyIncreasedBy200Percent()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "Premium" };
			var expectedMoney = 600m;

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}

		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsNotNormalSuperUserOrPremium_ShouldReturnSameMoney()
		{
			// Arrange
			var user = new User { Money = 50, UserType = "SomeOtherType" };
			var expectedMoney = 50m;

			// Act
			var result = _validationService.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}
	}
}
