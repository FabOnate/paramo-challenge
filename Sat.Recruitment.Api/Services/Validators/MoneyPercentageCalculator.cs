using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Services.Validators
{
    public class MoneyPercentageCalculator : IMoneyPercentageCalculator
    {
        public decimal CalculateMoneyPercentage(User user)
        {
            decimal moneyPercentage = user.Money;
            decimal percentage = 0;


            switch (user.UserType)
            {
                case "Normal":
                    if (user.Money > 100)
                    {
                        percentage = 0.12m;
                    }
                    else if (user.Money > 10)
                    {
                        percentage = 0.08m;
                    }
                    break;
                case "SuperUser":
                    if (user.Money > 100)
                    {
                        percentage = 0.20m;
                    }
                    break;
                case "Premium":
                    if (user.Money > 100)
                    {
                        moneyPercentage *= 3;
                    }
                    break;
                default:
                    return user.Money;
            }

            moneyPercentage += moneyPercentage * percentage;

            return moneyPercentage;
        }
    }
}

