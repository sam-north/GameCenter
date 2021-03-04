using Core.Framework.Mappers;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserGameController : ControllerBase
    {
        public IGameInstanceLogic GameInstanceLogic { get; }
        public IGameInstanceMapper GameInstanceMapper { get; }
        public IGameInstanceValidator GameInstanceValidator { get; }

        public UserGameController(IGameInstanceLogic gameInstanceLogic, IGameInstanceMapper gameInstanceMapper, IGameInstanceValidator gameInstanceValidator)
        {
            GameInstanceLogic = gameInstanceLogic;
            GameInstanceMapper = gameInstanceMapper;
            GameInstanceValidator = gameInstanceValidator;
        }

        [HttpPost]
        public ActionResult<Response<object>> New(CreateGameInstanceDto dto)
        {
            var validationResponse = GameInstanceValidator.Validate(dto);
            if (!validationResponse.IsValid) return BadRequest(validationResponse);

            var newGameInstanceResponse = GameInstanceLogic.New(dto);
            var response = ResponseMapper.Map<GameInstance, GameInstanceDto>(newGameInstanceResponse);
            response.Data = GameInstanceMapper.Map(newGameInstanceResponse.Data);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Response<object>> Get(Guid id)
        {
            var gameInstance = GameInstanceLogic.Get(id);
            if (gameInstance == null) return NotFound(gameInstance);

            var response = new Response<GameInstanceDto>();
            response.Data = GameInstanceMapper.Map(gameInstance);
            return Ok(response);
        }
    }
}
