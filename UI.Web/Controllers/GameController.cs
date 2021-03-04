using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        public IGameLogic GameLogic { get; }
        public IGameMapper GameMapper { get; }

        public GameController(IGameLogic gameLogic, IGameMapper gameMapper)
        {
            GameLogic = gameLogic;
            GameMapper = gameMapper;
        }

        [HttpGet]
        public ActionResult<ICollection<GameDto>> Get()
        {
            return Ok(GameMapper.Map(GameLogic.GetActiveGames()));
        }
    }
}
