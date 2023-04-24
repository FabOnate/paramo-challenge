using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Validators;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("ValidationTests", DisableParallelization = true)]
    public class MoneyPercentageCalculatorTests
	{
		private readonly IMoneyPercentageCalculator _moneyPercentageCalculator;


		public MoneyPercentageCalculatorTests()
		{
			_moneyPercentageCalculator = new MoneyPercentageCalculator();
		}
		
		[Fact]
		public void CalculateMoneyPercentage_WhenUserTypeIsNormalAndMoneyGreaterThan100_ShouldReturnMoneyIncreasedBy12Percent()
		{
			// Arrange
			var user = new User { Money = 200, UserType = "Normal" };
			var expectedMoney = 224m;

			// Act
			var result = _moneyPercentageCalculator.CalculateMoneyPercentage(user);

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
			var result = _moneyPercentageCalculator.CalculateMoneyPercentage(user);

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
			var result = _moneyPercentageCalculator.CalculateMoneyPercentage(user);

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
			var result = _moneyPercentageCalculator.CalculateMoneyPercentage(user);

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
			var result = _moneyPercentageCalculator.CalculateMoneyPercentage(user);

			// Assert
			Assert.Equal(expectedMoney, result);
		}
	}
}
