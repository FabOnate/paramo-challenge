using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Services
{
    public class ValidationService : IValidationService
    {

		public ValidationService()
		{
		}

		/// <summary>
		///    Validate if user- is duplicated
		/// </summary>
		/// <param name="users">List of user from file</param>
		/// <param name="user">user propeties</param>
		/// <returns>true/false</returns>
		public bool IsDuplicated(IList<User> users, User user)
        {
			return users.Any(existingUser => 
			(existingUser.Email == user.Email || existingUser.Phone == user.Phone) || 
			(existingUser.Name == user.Name && existingUser.Address == user.Address));

        }

		/// <summary>
		///    normalize email
		/// </summary>
		/// <param name="email">user email</param>
		/// <returns>string</returns>
		public string NormalizeEmail(string email)
		{

			var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
			var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
			aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Remove(atIndex).Replace(".", "").Trim();

			return string.Join("@", new string[] { aux[0], aux[1] }).Trim();

		}

		/// <summary>
		///    Calculate money percentage
		/// </summary>
		/// <param name="email">user email</param>
		/// <returns>calculated decimal</returns>
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
