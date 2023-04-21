using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services.Interface
{
    public interface IValidationService
    {
        bool IsDuplicated(IList<User> users, User user);
        string NormalizeEmail(string email);
        decimal CalculateMoneyPercentage(User user);

	}
}
