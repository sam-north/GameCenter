using Core.Logic.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IUserLogic UserLogic { get; }
        public UserController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<int> SignUp(User model)
        {
            var entity = UserLogic.Create(model);
            return CreatedAtAction("meh", new { id = entity.Id }, entity);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<int>> SignInAsync(User model)
        {
            var entity = UserLogic.SignIn(model);

            if (entity != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, entity.Email),
                    new Claim("Id", entity.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }


            return entity.Id;
            //return CreatedAtAction("meh", new { id = entity.Id }, entity);
        }

        //Controller/Action
        //User/SignIn

        //Class/Method
    }
}
