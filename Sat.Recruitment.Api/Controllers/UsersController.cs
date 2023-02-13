using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Model;
using Sat.Recruitment.Model.Dtos;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Services.Interfaces;
using System.Text.Json;
using FluentValidation;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserFactory _userFactory;
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _validator;

        public UsersController(IUserFactory userFactory, 
            IUserService userService, 
            IValidator<UserDto> validator)
        {
            _userFactory = userFactory;
            _userService = userService;
            _validator = validator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var resultValidation = _validator.Validate(userDto);
            if (resultValidation.IsValid)
            {
                var newUser = _userFactory.Resolve(Enum.Parse<UserType>(userDto.UserType)).Build(userDto);
                try
                {
                    if (await _userService.AddIfNotExist(newUser))
                    {
                        return CreatedAtAction(nameof(CreateUser), new { email = userDto.Email }, newUser);
                    }

                    return Conflict(new { message = $"The user is duplicated" });
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error Message{e.Message}");
                    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
            }

            var errors = JsonSerializer.Serialize(resultValidation.Errors.Select(errors => errors.ErrorMessage));
            return BadRequest(errors);
        }
    }
}
