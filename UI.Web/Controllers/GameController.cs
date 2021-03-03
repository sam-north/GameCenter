using Core.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GameController : ControllerBase
    {
        public IGameLogic GameLogic { get; }
        public GameController(IGameLogic gameLogic)
        {
            GameLogic = gameLogic;
        }
    }
}
