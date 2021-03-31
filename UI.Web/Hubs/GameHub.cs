using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace UI.Web.Hubs
{
    public class GameHub : Hub
    {
        private IRequestContext RequestContext { get; }
        private IGameInstanceUserMessageLogic GameInstanceUserMessageLogic { get; }
        private IGameInstanceMapper GameInstanceMapper { get; }
        private IGameInstanceValidator GameInstanceValidator { get; }

        public GameHub(IRequestContext requestContext,
            IGameInstanceUserMessageLogic gameInstanceUserMessageLogic,
            IGameInstanceMapper gameInstanceMapper,
            IGameInstanceValidator gameInstanceValidator)
        {
            RequestContext = requestContext;
            GameInstanceUserMessageLogic = gameInstanceUserMessageLogic;
            GameInstanceMapper = gameInstanceMapper;
            GameInstanceValidator = gameInstanceValidator;
        }

        public async Task GameUpdated(string gameInstanceId)
        {
            await Clients.Group(gameInstanceId).SendAsync("Refresh");
        }

        public async Task SubscribeToGame(string gameInstanceId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameInstanceId);
        }

        public async Task UnsubscribeFromGame(string gameInstanceId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameInstanceId);
        }

        public async Task SendMessage(ChatMessageDto dto)
        {
            var validationResults = GameInstanceValidator.Validate(dto);
            if (!validationResults.IsValid)
                throw new Exception(string.Join(',', validationResults.Errors));

            var messageToSave = new GameInstanceUserMessage();
            messageToSave.Text = dto.Text;
            messageToSave.UserId = RequestContext.UserId;
            messageToSave.GameInstanceId = dto.Id;
            var entity = GameInstanceUserMessageLogic.Save(messageToSave);
            var responseDto = GameInstanceMapper.Map(entity);
            responseDto.UserEmail = RequestContext.Email;

            await Clients.Group(dto.Id.ToString()).SendAsync("ReceiveMessage", responseDto);
        }
    }
}
