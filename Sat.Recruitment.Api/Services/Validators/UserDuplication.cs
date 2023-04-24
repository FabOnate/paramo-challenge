using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Services.Validators
{
    public class UserDuplication : IUserDuplicationValidator
    {
        public bool IsDuplicated(IList<User> users, User user)
        {
            return users.Any(existingUser =>
                existingUser.Email == user.Email || existingUser.Phone == user.Phone ||
                existingUser.Name == user.Name && existingUser.Address == user.Address);
        }
    }
}
