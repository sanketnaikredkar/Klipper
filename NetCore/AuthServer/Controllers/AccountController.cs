// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using AuthServer.Controllers;
using IdentityServer4;
using AuthServer.DataAccess.Database;
using AuthServer.DataAccess;

namespace AuthServer.Controllers
{
    [SecurityHeaders]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IUserRepository _userRepository;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _interaction = interaction;
            _clientStore = clientStore;
            //_account = new AccountService(interaction, httpContextAccessor, clientStore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                // validate username/password against in-memory store
                if (_userRepository.ValidateCredentials(user.UserName, user.PasswordHash))
                {
                    Microsoft.AspNetCore.Authentication.AuthenticationProperties props = null;

                    // only set explicit expiration here if persistent. 
                    // otherwise we reply upon expiration configured in cookie middleware.
                    props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };

                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String),
                        new Claim(ClaimTypes.Role, "HRRepresentative", ClaimValueTypes.String) //For time being give access to all
                    };
                    // issue authentication cookie with subject ID and username
                    var currentUser = _userRepository.GetByUserName(user.UserName);
                    //await AuthenticationHttpContextExtensions.SignInAsync(claimsPrincipal);

                    var subjectId = user.ID.ToString();
                    await HttpContext.SignInAsync(subjectId, props, claims);

                    return Ok();
                }

                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}