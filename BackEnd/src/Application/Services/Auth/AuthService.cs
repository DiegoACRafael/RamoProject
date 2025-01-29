using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Request.Auth;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        public async Task<string> Login(LoginUserRequest loginUser)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegisterUserRequest registerUser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}