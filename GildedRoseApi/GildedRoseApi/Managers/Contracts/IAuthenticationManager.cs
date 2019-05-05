using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseData.Entities;

namespace GildedRoseApi.Managers.Contracts
{
    public interface IAuthenticationManager
    {
        IEnumerable<GildedRoseUser> GetAllLogins();

        Task<GildedRoseToken> Login(AuthenticationModel authModel);

    }
}
