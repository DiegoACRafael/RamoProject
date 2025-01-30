using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Auth;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Register(RegisterUserRequest registerUser);

        Task<string> Login(LoginUserRequest loginUser);
    }
}