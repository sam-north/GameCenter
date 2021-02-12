using Core.Logic.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public ActionResult<int> Post(User model)
        {
            var entity = UserLogic.Create(model);
            return CreatedAtAction("meh", new { id = entity.Id }, entity);
        }
    }
}
