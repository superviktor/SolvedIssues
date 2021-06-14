using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Authentiocation_and_Authorization.Contract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentiocation_and_Authorization
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "viktor",
                Password = "password"
            }
        };

        private readonly AuthSettings _authSettings;

        public UserService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }


        public AuthResponse Authenticate(AuthRequest request)
        {
            var user = _users.SingleOrDefault(x => x.Name == request.UserName && x.Password == request.Password);

            if (user == null)
                return null;

            var token = GenerateJwt(user);

            return new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                Token = token
            };
        }

        public User GetById(int userId)
        {
            return _users.FirstOrDefault(x => x.Id == userId);
        }

        private string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}