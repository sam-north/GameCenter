using Core.Framework.Mappers;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
        public ActionResult<IResponse<object>> New(CreateGameInstanceDto dto)
        {
            var validationResponse = GameInstanceValidator.Validate(dto);
            if (!validationResponse.IsValid) return BadRequest(validationResponse);

            var gameInstanceResponse = GameInstanceLogic.New(dto);

            var response = ResponseMapper.MapMetadata<GameInstanceDto>(gameInstanceResponse);
            response.Data = GameInstanceMapper.Map(gameInstanceResponse.Data);
            return Ok(response);
        }

        [HttpGet]
        public ActionResult<IResponse<object>> Get()
        {
            var gameInstances = GameInstanceLogic.GetCurrentUserGameInstancesWithoutGameStates();
            if (gameInstances == null) return new EmptyResult();

            var response = new Response<ICollection<UserGameInstanceStatelessDto>>();
            response.Data = GameInstanceMapper.Map(gameInstances);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IResponse<object>> Get(Guid id)
        {
            var gameInstance = GameInstanceLogic.Get(id);
            if (gameInstance == null) return NotFound(gameInstance);

            var response = new Response<GameInstanceDto>();
            response.Data = GameInstanceMapper.Map(gameInstance);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<IResponse<object>> Play(PlayGameInstanceDto dto)
        {
            var validationResponse = GameInstanceValidator.Validate(dto);
            if (!validationResponse.IsValid) return BadRequest(validationResponse);

            var gameInstanceResponse = GameInstanceLogic.Play(dto);

            var response = ResponseMapper.MapMetadata<GameInstanceDto>(gameInstanceResponse);
            response.Data = GameInstanceMapper.Map(gameInstanceResponse.Data);
            return Ok(response);
        }
    }
}
