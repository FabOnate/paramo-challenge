using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Services.Validators
{
    public interface IMoneyPercentageCalculator
	{
		decimal CalculateMoneyPercentage(User user);
	}
}
