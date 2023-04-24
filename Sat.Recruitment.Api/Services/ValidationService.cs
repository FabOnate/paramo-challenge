using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using Sat.Recruitment.Api.Services.Validators;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services
{
    public class ValidationService : IValidationService
    {

		private readonly IUserDuplicationValidator _userDuplicationValidator;
		private readonly IEmailNormalizer _emailNormalizer;
		private readonly IMoneyPercentageCalculator _moneyPercentageCalculator;

		public ValidationService(IUserDuplicationValidator userDuplicationValidator,
			IEmailNormalizer emailNormalizer, IMoneyPercentageCalculator moneyPercentageCalculator)
		{
			_userDuplicationValidator = userDuplicationValidator;
			_emailNormalizer = emailNormalizer;
			_moneyPercentageCalculator = moneyPercentageCalculator;
		}

		/// <summary>
		///    Validate if user is duplicated
		/// </summary>
		/// <param name="users">List of user from file</param>
		/// <param name="user">user propeties</param>
		/// <returns>true/false</returns>
		public bool IsDuplicated(IList<User> users, User user)
        {
			return _userDuplicationValidator.IsDuplicated(users, user);
		}

		/// <summary>
		///    normalize email
		/// </summary>
		/// <param name="email">user email</param>
		/// <returns>string</returns>
		public string NormalizeEmail(string email)
		{
			return _emailNormalizer.NormalizeEmail(email);
		}

		/// <summary>
		///    Calculate money percentage
		/// </summary>
		/// <param name="email">user email</param>
		/// <returns>calculated decimal</returns>
		public decimal CalculateMoneyPercentage(User user)
        {
			return _moneyPercentageCalculator.CalculateMoneyPercentage(user);
		}
	}
}
