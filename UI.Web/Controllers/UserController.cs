using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        IUserLogic UserLogic { get; }
        IUserMapper UserMapper { get; }
        IUserValidator UserValidator { get; }

        public UserController(IUserLogic userLogic, IUserMapper userMapper, IUserValidator userValidator)
        {
            UserLogic = userLogic;
            UserMapper = userMapper;
            UserValidator = userValidator;
        }

        [HttpPost]
        public ActionResult<Response<object>> SignUp(UserSignUpDto dto)
        {
            var validationResponse = UserValidator.Validate(dto);
            if (!validationResponse.IsValid)
                return BadRequest(validationResponse);

            var entity = UserMapper.Map(dto);
            UserLogic.Create(entity);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Response<object>>> SignInAsync(UserSignInDto dto)
        {
            var validationResponse = UserValidator.Validate(dto);
            if (!validationResponse.IsValid)
                return BadRequest(validationResponse);

            var attemptCredentialsResponse = UserLogic.SignIn(UserMapper.Map(dto));
            if (!attemptCredentialsResponse.IsValid)
                return BadRequest(attemptCredentialsResponse);

            if (attemptCredentialsResponse != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(nameof(RequestContext.Email), attemptCredentialsResponse.Data.Email),
                    new Claim(nameof(RequestContext.UserId), attemptCredentialsResponse.Data.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.Now,
                    RedirectUri = "User/SignIn"
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Response<object>>> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }
    }
}
