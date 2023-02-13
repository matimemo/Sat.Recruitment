using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Configuration;
using Xunit;

namespace Sat.Recruitment.DAL.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task When_It_Calls_To_Exist_And_The_User_Exist_In_The_File()
        {
            //arrange
            var configuration = new FileConfiguration()
            {
                FilePath = "/Files/",
                FileName = "FileToCheckIfUserExist.txt"
            };

            var user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Garay y Otra Calle",
                Phone = "+534645213542",
                UserType = UserType.SuperUser,
                Money = 112234
            };

            var dao = new UserRepository(new OptionsWrapper<FileConfiguration>(configuration));

            //act
            await dao.Exists(user);

            //assert
            Assert.True(await dao.Exists(user));
        }

        [Fact]
        public async Task When_It_Calls_To_Add_With_It_Add_User_To_File()
        {
            //arrange
            var configuration = new FileConfiguration()
            {
                FilePath = "/Files/",
                FileName = "FileToAddUsers.txt"
            };

            var user = new User()
            {
                Name = "Matias",
                Email = "matiasmemolli@gmail.com",
                Address = "9 968",
                Phone = "2215920970",
                UserType = UserType.Normal,
                Money = 1000
            };

            var user2 = new User()
            {
                Name = "Julieta",
                Email = "julietamoyano@gmail.com",
                Address = "8 548",
                Phone = "2215920980",
                UserType = UserType.SuperUser,
                Money = 2000
            };

            var dao = new UserRepository(new OptionsWrapper<FileConfiguration>(configuration));

            //act
            await dao.Add(user);
            await dao.Add(user2);

            //assert
            Assert.True(await dao.Exists(user));
            Assert.True(await dao.Exists(user2));
            CleanFileToAdd(configuration);
        }
        private void CleanFileToAdd(FileConfiguration configuration)
        {
            var path = Directory.GetCurrentDirectory() + configuration.FilePath + configuration.FileName;
            File.WriteAllText(path, "");
        }
    }
}
