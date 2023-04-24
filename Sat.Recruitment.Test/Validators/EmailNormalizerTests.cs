using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interface;
using Sat.Recruitment.Api.Services.Validators;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("ValidationTests", DisableParallelization = true)]
    public class EmailNormalizerTests
	{
		private readonly IEmailNormalizer _emailNormalizer;


		public EmailNormalizerTests()
		{
			_emailNormalizer = new EmailNormalizer();
		}

		[Theory]
		[InlineData("user@example.com", "user@example.com")]
		[InlineData("user.email@example.com", "useremail@example.com")]
		[InlineData("user.email+ @example.com", "useremail@example.com")]
		public void NormalizeEmail_ShouldNormalizeEmail(string originalEmail, string expectedEmail)
		{
			// Arrange

			// Act
			var normalizedEmail = _emailNormalizer.NormalizeEmail(originalEmail);

			// Assert
			Assert.Equal(expectedEmail, normalizedEmail);
		}
	}
}
