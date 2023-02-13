using System.Collections.Generic;
using Sat.Recruitment.Model;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;

namespace Sat.Recruitment.Services
{
    public class SuperUserBuilder : UserBuilder, ISuperUserBuilder
    {
        public SuperUserBuilder(IEmailService emailService) : base(emailService)
        {
        }


        protected override UserType SetUserType()
        {
            return UserType.SuperUser;
        }

        protected override List<PercentageByCondition> FillPercentageConditions()
        {
            return new List<PercentageByCondition>()
            {
                new PercentageByCondition()
                {
                    Percentage = 0.20M,
                    Condition = arg => arg > 100
                }
            };
        }
    }
}