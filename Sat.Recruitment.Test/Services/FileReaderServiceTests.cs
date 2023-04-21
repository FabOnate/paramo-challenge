using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Services
{
    [CollectionDefinition("FileReaderTests", DisableParallelization = true)]
    public class FileReaderServiceTests
	{
		private static string currentDir = "";

		public FileReaderServiceTests()
		{
			currentDir = Environment.CurrentDirectory;
		}

		[Fact]
		public async Task ReadUsersFromFile_ShouldReturnListOfUsers_WhenFileExists()
		{
			// Arrange
			var _fileReaderService = new FileReaderService();

			// Act
			var users = await _fileReaderService.ReadUsersFromFile();

			// Assert
			Assert.NotNull(users);
			Assert.True(users.Any());
			Assert.IsType<User>(users.FirstOrDefault());
		}

		[Fact]
		public void WriteFile_ShouldAppendToFile_WhenValidUserProvided()
		{
			var _fileReaderService = new FileReaderService();
			// Arrange
			var user = new User
			{
				Name = "test",
				Email = "test@example.com",
				Phone = "1234567890",
				Address = "123 House",
				UserType = "Normal",
				Money = 100
			};
			var expectedRow = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";

			var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
			if (File.Exists(path))
			{
				File.Delete(path);
			}

			// Act
			_fileReaderService.WriteFile(user);

			// Assert
			Assert.True(File.Exists(path));
			var lines = File.ReadAllLines(path);
			Assert.Equal(expectedRow, lines[1]);
		}

		[Fact]
		public async Task ReadUsersFromFile_ShouldThrowFileReadingException_WhenFileDoesNotExist()
		{
			var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(tempDir);
			currentDir = Environment.CurrentDirectory;
			Environment.CurrentDirectory = tempDir;

			var _fileReaderService = new FileReaderService();
			// Act & Assert
			try
			{
				// Act & Assert
				await Assert.ThrowsAsync<FileReadingException>(() => _fileReaderService.ReadUsersFromFile());
			}
			finally
			{
				Environment.CurrentDirectory = currentDir;
			}

		}
	}
}
