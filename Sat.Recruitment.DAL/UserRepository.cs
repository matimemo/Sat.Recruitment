using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Configuration;

namespace Sat.Recruitment.DAL
{
    public class UserRepository :IUserRepository
    {
        private FileConfiguration _configuration;
        
        public UserRepository(IOptions<FileConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task Add(User user)
        {
            var path = GetFullFileName();

            using (var fileWriter = File.AppendText(path))
            {
                await fileWriter.WriteLineAsync(user.ToString());
            }

        }
        public async Task<bool> Exists(User user)
        {
            using (var reader = GetStremReader())
            {
                var exists = false;
                while (reader.Peek() >= 0 && !exists)
                {
                    var lineUserSaved = await reader.ReadLineAsync();
                    var userSaved = lineUserSaved.ToUser();
                    if (userSaved == null) continue;
                    exists = user.Equals(userSaved);
                }
                return exists;
            }
        }
        private StreamReader GetStremReader()
        {
            var path = GetFullFileName();
            var reader = new StreamReader(new FileStream(path, FileMode.Open));
            return reader;
        }
        private string GetFullFileName()
        {
            return Directory.GetCurrentDirectory() + _configuration.FilePath + _configuration.FileName;
        }
    }
}