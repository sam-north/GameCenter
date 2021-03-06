﻿using Core.Framework.Mappers;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Web.Hubs;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserGameController : ControllerBase
    {
        private IGameInstanceLogic GameInstanceLogic { get; }
        private IGameInstanceMapper GameInstanceMapper { get; }
        private IGameInstanceValidator GameInstanceValidator { get; }
        private IGameInstanceUserMessageLogic GameInstanceUserMessageLogic { get; }
        public IHubContext<GameHub> GameHubContext { get; }

        public UserGameController(IGameInstanceLogic gameInstanceLogic,
            IGameInstanceMapper gameInstanceMapper,
            IGameInstanceValidator gameInstanceValidator,
            IGameInstanceUserMessageLogic gameInstanceUserMessageLogic,
            IHubContext<GameHub> gameHubContext)
        {
            GameInstanceLogic = gameInstanceLogic;
            GameInstanceMapper = gameInstanceMapper;
            GameInstanceValidator = gameInstanceValidator;
            GameInstanceUserMessageLogic = gameInstanceUserMessageLogic;
            GameHubContext = gameHubContext;
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("{id}")]
        public ActionResult<IResponse<object>> Get(Guid id)
        {
            var gameInstance = GameInstanceLogic.Get(id);
            if (gameInstance == null) return NotFound(gameInstance);

            var response = new Response<GameInstanceDto>();
            response.Data = GameInstanceMapper.Map(gameInstance);
            return Ok(response);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("{id}")]
        public ActionResult<IResponse<object>> GetChat(Guid id)
        {
            var gameInstanceMessages = GameInstanceUserMessageLogic.GetViewResults(id);
            if (gameInstanceMessages == null) return NotFound(gameInstanceMessages);

            var response = new Response<ICollection<GameInstanceUserMessageDto>>();
            response.Data = GameInstanceMapper.Map(gameInstanceMessages);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<IResponse<object>>> PlayAsync(PlayGameInstanceDto dto)
        {
            var validationResponse = GameInstanceValidator.Validate(dto);
            if (!validationResponse.IsValid) return BadRequest(validationResponse);

            var gameInstanceResponse = GameInstanceLogic.Play(dto);

            var response = ResponseMapper.MapMetadata<GameInstanceDto>(gameInstanceResponse);
            response.Data = GameInstanceMapper.Map(GameInstanceLogic.Get(dto.Id));
            if (response.IsValid)
                await GameHubContext.Clients.Group(dto.Id.ToString()).SendAsync("Refresh");
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<IResponse<object>> RefreshCheck(RefreshCheckDto dto)
        {
            var gameInstance = GameInstanceLogic.Get(dto.Id);
            if (gameInstance == null) return NotFound(gameInstance);

            var response = new Response<GameInstanceDto>();

            if (gameInstance.State.DateCreated <= dto.Date)
                return Ok();

            response.Data = GameInstanceMapper.Map(gameInstance);
            return Ok(response);
        }
    }
}
