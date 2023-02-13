using System.Linq;
using Sat.Recruitment.Services.Interfaces;

namespace Sat.Recruitment.Services
{
    public class EmailService : IEmailService
    {
        public string Normalize(string email)
        {
            var emailSections = email.Split(new char[] { '@' });

            var emailNameSection = emailSections[0];

            emailNameSection = ReplaceNameDots(emailNameSection);

            emailNameSection = ExtractPlusSection(emailNameSection);

            return $"{emailNameSection}@{emailSections[1]}";
        }

        private static string ReplaceNameDots(string emailNameSection)
        {
            return emailNameSection.Replace(".", "");
        }

        private static string ExtractPlusSection(string emailNameSection)
        {
            if (emailNameSection.Any(letter => letter == '+'))
            {
                emailNameSection = emailNameSection.Replace("+", string.Empty);
            }

            return emailNameSection;
        }
    }
}
