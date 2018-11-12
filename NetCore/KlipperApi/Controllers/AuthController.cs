// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Models.Core.Authentication;
using KlipperApi.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace KlipperApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeManager _employeeManager;

        public AuthController(IUserRepository userRepository, IEmployeeManager employeeManager)
        {
            _userRepository = userRepository;
            _employeeManager = employeeManager;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if(_userRepository.GetByUserName(user.UserName) == null)
            {
                Serilog.Log.Logger.Error("Username is not found.");
                return NotFound();
            }

            if (_userRepository.ValidateCredentials(user.UserName, user.PasswordHash))
            {
                // only set explicit expiration here if persistent. 
                // otherwise we reply upon expiration configured in cookie middleware.
                var props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3650)
                };

                var employee = _employeeManager.GetEmployeeAsync(user.ID).Result;
                var roles = employee.Roles;

                var claims = new List<Claim>();

                foreach (var r in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r.Role, ClaimValueTypes.String));
                }
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String));
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KlipperSigningKey"));

                var token = new JwtSecurityToken(
                    issuer: "http://www.Klingelnberg.com",
                    audience: "http://www.Klingelnberg.com",
                    expires: DateTime.UtcNow.AddDays(5),
                    claims: claims.ToArray(),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                //KK: Also try this...
                //var currentUser = _userRepository.GetByUserName(user.UserName);
                //await AuthenticationHttpContextExtensions.SignInAsync(claimsPrincipal);

                var subjectId = user.ID.ToString();
                await HttpContext.SignInAsync(subjectId, claims.ToArray());

                return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
            }
            ModelState.AddModelError("", "Error in user authentication");
            Serilog.Log.Logger.Error("Error in user authentication");

            return Unauthorized();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return Ok();
        //}
    }
}