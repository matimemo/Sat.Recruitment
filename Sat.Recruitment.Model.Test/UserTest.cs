using Xunit;

namespace Sat.Recruitment.Model.Test
{
    public class UserTest
    {
        [Fact]
        public void When_You_Call_ToString_Should_Return_The_Properties_In_One_Line()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            //act
            var result = user.ToString();

            //assert
            Assert.Equal($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}",result);
        }

        [Fact]
        public void When_You_Call_Equals_With_Same_Email_Should_Return_True()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            var user2 = new User()
            {
                Name = "Matias A",
                Address = "9 968 3",
                Email = "matiasmemolli@gmail.com",
                Money = 2000,
                Phone = "35920970",
                UserType = UserType.Premium
            };
            //act
            var result = user.Equals(user2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void When_You_Call_Equals_With_Same_Phone_Should_Return_True()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            var user2 = new User()
            {
                Name = "Matias A",
                Address = "9 968 3",
                Email = "matiasmemolli1@gmail.com",
                Money = 2000,
                Phone = "5920970",
                UserType = UserType.Premium
            };
            //act
            var result = user.Equals(user2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void When_You_Call_Equals_With_Same_Name_And_Address_Should_Return_True()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            var user2 = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli1@gmail.com",
                Money = 2000,
                Phone = "35920970",
                UserType = UserType.Premium
            };
            //act
            var result = user.Equals(user2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void When_You_Call_Equals_With_Same_Name_And_Different_Address_Should_Return_False()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            var user2 = new User()
            {
                Name = "Matias A",
                Address = "9 968 1",
                Email = "matiasmemolli1@gmail.com",
                Money = 2000,
                Phone = "35920970",
                UserType = UserType.Premium
            };
            //act
            var result = user.Equals(user2);

            //assert
            Assert.False(result);
        }


        [Fact]
        public void When_You_Call_Equals_With_Same_Adress_Should_Return_False()
        {
            //arrange
            var user = new User()
            {
                Name = "Matias",
                Address = "9 968",
                Email = "matiasmemolli1@gmail.com",
                Money = 1000,
                Phone = "5920970",
                UserType = UserType.Normal
            };
            var user2 = new User()
            {
                Name = "Matias A",
                Address = "9 968",
                Email = "matiasmemolli@gmail.com",
                Money = 2000,
                Phone = "35920970",
                UserType = UserType.Premium
            };
            //act
            var result = user.Equals(user2);

            //assert
            Assert.False(result);
        }
    }
}
