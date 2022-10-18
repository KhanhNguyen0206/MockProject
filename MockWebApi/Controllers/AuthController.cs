using Domain.Dto.UserDto;
using Domain.Models;
using Domain.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace MockWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private IValidator<UserRegisterDto> _validator;
        public AuthController(IAuthRepository authRepo, IValidator<UserRegisterDto> validator)
        {
            _authRepo = authRepo;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register ( UserRegisterDto user)
        {
            UsersValidator rules = new();
            var validator = rules.Validate(user);
            if (!validator.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validator.Errors);
            }
            var response = await _authRepo.Register(user);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin user)
        {
            var response = await _authRepo.Login(user.UserName, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
