using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoleBasedAuth.Contract;

namespace RoleBasedAuth
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new()
            {
                Id = 1,
                Name = "viktor",
                Password = "password",
                Role = Role.Admin
            },
            new()
            {
                Id = 2,
                Name = "vasyl",
                Password = "pwd",
                Role = Role.User
            }
        };

        private readonly AuthSettings _authSettings;

        public UserService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value;
        }


        public User Authenticate(AuthRequest request)
        {
            var user = _users.SingleOrDefault(x => x.Name == request.UserName && x.Password == request.Password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public User GetById(int userId)
        {
            return _users.FirstOrDefault(x => x.Id == userId).WithoutPassword();
        }
    }
}