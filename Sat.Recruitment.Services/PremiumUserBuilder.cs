using System.Collections.Generic;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;

namespace Sat.Recruitment.Services
{
    public class PremiumUserBuilder : UserBuilder, IPremiumUserBuilder
    {
        public PremiumUserBuilder(IEmailService emailService) : base(emailService)
        {
        }


        protected override UserType SetUserType()
        {
            return UserType.Premium;
        }

        protected override List<PercentageByCondition> FillPercentageConditions()
        {
            return new List<PercentageByCondition>()
            {
                new PercentageByCondition()
                {
                    Percentage = 2,
                    Condition = arg => arg > 100
                }
            };
        }
    }
}