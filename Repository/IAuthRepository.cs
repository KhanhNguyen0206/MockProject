using Domain;
using Domain.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(UserRegisterDto user);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
