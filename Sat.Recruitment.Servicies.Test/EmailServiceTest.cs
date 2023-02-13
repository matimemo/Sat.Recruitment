using System.Threading.Tasks;
using Sat.Recruitment.Services;
using Xunit;

namespace Sat.Recruitment.Servicies.Test
{
    public class EmailServiceTest
    {
        private EmailService service;
        public EmailServiceTest()
        {
            service = new EmailService();
        }

        [Theory]
        [InlineData("matias.memolli@gmail.com", "matiasmemolli@gmail.com")]
        [InlineData(".julieta.moyano.@gmail.com", "julietamoyano@gmail.com")]
        public async Task When_Normalize_Is_Called_It_Removes_Dot_Correctly(string sourceEmail, string emailExpected)
        {
            //arrange

            //act
            var result = service.Normalize(sourceEmail);

            //assert
            Assert.Equal(emailExpected, result);
        }

        [Theory]
        [InlineData("+matiasmemolli@gmail.com", "matiasmemolli@gmail.com")]
        [InlineData(".julieta+moyano.@gmail.com", "julietamoyano@gmail.com")]
        public async Task When_Normalize_Is_Called_It_Removes_Plus_Sign_And_Dots_Correctly(string sourceEmail, string emailExpected)
        {
            //arrange

            //act
            var result = service.Normalize(sourceEmail);

            //assert
            Assert.Equal(emailExpected, result);
        }
    }
}
