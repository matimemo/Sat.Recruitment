using FluentValidation;
using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Dtos;

namespace Sat.Recruitment.Api.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            Include(new UserRequiredFieldsValidations());
            RuleFor(x => x.Money).Must(BeANumber).WithMessage("You must put a number in field Money");
            RuleFor(x => x.UserType).IsEnumName(typeof(UserType)).WithMessage("You must put a valid User Type in field User Type");
            RuleFor(x => x.Email).EmailAddress().WithMessage("the mail is invalid");
        }

        private bool BeANumber(string money)
        {
            return decimal.TryParse(money, out var result);
        }

    }

    public class UserRequiredFieldsValidations : AbstractValidator<UserDto>
    {
        public UserRequiredFieldsValidations()
        {
            RuleFor(x => x.UserType).NotNull().NotEmpty().WithMessage("UserType is required");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Money).NotNull().NotEmpty().WithMessage("Money is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Phone is required");
        }
    }
}
