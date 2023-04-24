using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public partial class FileService : IFileService
    {
		public FileService()
		{
		}

		/// <summary>
		///    ReadUserFile
		/// </summary>
		/// <returns>IList of users</returns>
		public async Task<IList<User>> ReadUsersFromFile()
		{
			try
			{
				List<User> _users = new List<User>();
				var reader = ReadFile();
				while (reader.Peek() >= 0)
				{
					var line = await reader.ReadLineAsync();
					if (line?.Length != null && line?.Length > 0)
					{
						var userPerLine = new User
						{
							Name = line.Split(',')[0].ToString(),
							Email = line.Split(',')[1].ToString(),
							Phone = line.Split(',')[2].ToString(),
							Address = line.Split(',')[3].ToString(),
							UserType = line.Split(',')[4].ToString(),
							Money = decimal.Parse(line.Split(',')[5].ToString()),
						};
						_users.Add(userPerLine);
					}
				}

				reader.Close();
				return _users;
			}
			catch (Exception ex)
			{
				throw new FileReadingException("An error was found while reading the file. ", ex);
			}
		}
		private StreamReader ReadFile()
		{
			var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

			FileStream fileStream = new FileStream(path, FileMode.Open);

			StreamReader reader = new StreamReader(fileStream);
			return reader;
		}

		public void WriteFile(User user)
		{
			var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
			string row = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money.ToString()}";

			using (StreamWriter writer = File.AppendText(path))
			{
				writer.WriteLine();
				writer.WriteLine(row);
			}
		}
	}
}
