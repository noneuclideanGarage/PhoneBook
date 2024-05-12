using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Models;
using Serilog;

namespace PhoneBook.WebApi.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["JWT:SigningKey"]
                ?? throw new ArgumentException("Cannot get signing key")
            )
        );
    }

    public string? CreateToken(AppUser user)
    {
        try
        {
            List<Claim> claims =
            [
                new Claim(
                    JwtRegisteredClaimNames.GivenName, 
                    user.UserName 
                    ?? throw new ArgumentException("User's name equals null"))
            ];

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            return null;
        }
    }
}