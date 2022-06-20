using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using API.Models;
using API.DTOs;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(ILogger<AccountController> logger, SignInManager<AppUser> signInManger,
        UserManager<AppUser> userManager, ITokenService tokenService )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManger;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUserDTO>> Login(LoginModel loginModel)
        {
            await _signInManager.SignOutAsync();
             if (ModelState.IsValid)
            {
                var user =
                await _userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                         return new AppUserDTO{
                            UserName = loginModel.UserName,
                            Token = _tokenService.CreateToken(user)
                         };
                    }
                }
            }
            return StatusCode(500, "Invalid email or password");
        }
        [HttpPost("register")]
        public async Task<ActionResult<AppUserDTO>> Register(RegisterModel registerModel)
        {
            await _signInManager.SignOutAsync();
            var checkuser = await _userManager.FindByNameAsync(registerModel.UserName);
            if(checkuser != null)
                 return StatusCode(500, $"User with UserName '{registerModel.UserName}' Already Exists");

            string errors = "";
             if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    return new AppUserDTO{
                            UserName = registerModel.UserName,
                            Token = _tokenService.CreateToken(user)
                         };
                }
                else
                {
                    foreach (var error in result.Errors.Select(x => x.Description))
                    {
                        errors +=  error + "\n";
                    }
                }
            }
            return StatusCode(401, errors);

        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return StatusCode(200, "Logout Success!");
        }

    
    }
}