using Core.Framework.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace UI.Web.Hubs
{
    public class GameHub : Hub
    {
        public IRequestContext RequestContext { get; }
        public GameHub(IRequestContext requestContext)
        {
            RequestContext = requestContext;
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

        public async Task SendMessage(string gameInstanceId, string message)
        {
            var user = RequestContext.Email;
            await Clients.Group(gameInstanceId).SendAsync("ReceiveMessage", user, message);
        }
    }
}
