using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GildedRoseApi.Managers.Contracts;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GildedRoseApi.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        //Todo: move settings to a .config file

        private readonly GildedRoseUserContext _context;
        private UserManager<GildedRoseUser> _userManager;
        private IConfiguration _config;

        public AuthenticationManager(GildedRoseUserContext context, UserManager<GildedRoseUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _config = configuration;
        }

        public  IEnumerable<GildedRoseUser> GetAllLogins()
        {
            return  _context.GildedRoseUsers;
        }


        public async Task<GildedRoseToken> Login(AuthenticationModel authModel)
        {
            List<Claim> claims = null;
            var user = await _userManager.FindByNameAsync(authModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, authModel.Password))
            {
                //user is authenticated.  If they are admin allow them to get a list of the available users
                if (user.UserName == "admin")
                {
                    claims = new List<Claim>() {new Claim(ClaimTypes.Role, "Administrator")};
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt8&j^&"]));

                var token = new JwtSecurityToken(
                    issuer: _config["jwtIssuer"],
                    audience: _config["jwtAudience"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                );
                return new GildedRoseToken(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
            }
            return null;
        }
    }

    public class GildedRoseToken
    {
        public GildedRoseToken(string token, DateTime validTo )
        {
            Token = token;
            Expiration = validTo;

        }

        public string Token { get; }
        public DateTime Expiration { get; }
    }

}
