using Xunit;

namespace Sat.Recruitment.Model.Test
{
    public class UserHelperClass
    {
        [Fact]
        public void When_You_Call_ToUser_Should_Return_A_User_From_Line()
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
            var userString = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";

            //act
            var result = userString.ToUser();

            //assert
            Assert.Equal(user.Name,result.Name);
            Assert.Equal(user.Address,result.Address);
            Assert.Equal(user.Email,result.Email);
            Assert.Equal(user.Money,result.Money);
            Assert.Equal(user.Phone,result.Phone);
            Assert.Equal(user.UserType,result.UserType);
        }
    }
}