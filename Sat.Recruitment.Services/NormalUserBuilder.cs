using System.Collections.Generic;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;

namespace Sat.Recruitment.Services
{
    public class NormalUserBuilder : UserBuilder, INormalUserBuilder
    {
        
        public NormalUserBuilder(IEmailService emailService) : base(emailService)
        {
            
        }

        protected override UserType SetUserType()
        {
            return UserType.Normal;
        }

        protected override List<PercentageByCondition> FillPercentageConditions()
        {
            return new List<PercentageByCondition>()
            {
                new PercentageByCondition()
                {
                    Percentage = 0.12M,
                    Condition = arg => arg > 100
                },
                new PercentageByCondition()
                {
                    Percentage = 0.8M,
                    Condition = arg => (arg < 100 && arg > 10)
                }
            };
        }
    }
}