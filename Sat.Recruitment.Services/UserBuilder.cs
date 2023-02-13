using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Dtos;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Interfaces.BuilderInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Services
{
    public abstract class UserBuilder : IUserBuilder
    {
        private readonly IEmailService _emailService;
        protected List<PercentageByCondition> _percentageConditions;
        protected abstract UserType SetUserType();
        protected abstract List<PercentageByCondition> FillPercentageConditions();

        public UserBuilder(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        public User Build(UserDto userDto)
        {
            var money = decimal.Parse(userDto.Money);
            _percentageConditions = FillPercentageConditions();
            return new User
            {
                Name = userDto.Name,
                Email = _emailService.Normalize(userDto.Email),
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = SetUserType(),
                Money = CalculateMoney(money)
            };
        }

        private decimal CalculateMoney(decimal money)
        {
            var applyedCondition = _percentageConditions.FirstOrDefault(ac => ac.Condition.Invoke(money));
            if (applyedCondition != null)
            {
                var gif = money * applyedCondition.Percentage;
                money += +gif;
            }

            return money;
        }
    }
}
