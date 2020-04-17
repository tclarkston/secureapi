using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration;

        public UserService(IConfiguration configuration) {
            _configuration = configuration;
        }

        private List<User> _users = new List<User>
        {
            new User { Username = "test", Password = "test" }
        };
    
        public Token Authenticate(string username, string password)
        {
            //TODO: Change this to get a real user from a DB
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }
            
            var exp = DateTime.Now.AddMinutes(30).ToUniversalTime();
            var nbf = DateTime.Now.ToUniversalTime();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(nbf).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(exp).ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:SecretKey"))),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new Token
            {
                Value = new JwtSecurityTokenHandler().WriteToken(token),
                Exp = exp,
                Nbf = nbf   
            };
        }
    }
}