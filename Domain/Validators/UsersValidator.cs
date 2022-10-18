using Domain.Dto.UserDto;
using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class UsersValidator : AbstractValidator<UserRegisterDto>
    {
        public UsersValidator()
        {
            RuleFor(x => x.DisplayName).Length(2, 30).NotNull().WithMessage("DisplayName cannot be null or empty");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Please enter the email!"); ;
            RuleFor(x => x.Password).NotNull().WithMessage("Password is null or empty");
        }
    }
}
