using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GildedRoseApi.Managers.Contracts;
using GildedRoseData.Entities;
using GildedRoseData.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GildedRoseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _manager;

        public AuthenticationController(IAuthenticationManager manager)
        {
            _manager = manager;
        }

        // GET: api/Authentication/logins
        [Authorize(Roles = "Administrator")]
        [Route("logins")]
        [HttpGet]
        public IEnumerable<GildedRoseUser> Get()
        {
            return _manager.GetAllLogins();
        }


        // POST: api/Authentication/logins
        [HttpPost]
        [Route("logins")]
        public async Task<IActionResult> Post([FromBody] AuthenticationModel authModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginResult = await _manager.Login(authModel);

            if (loginResult != null)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }
}
