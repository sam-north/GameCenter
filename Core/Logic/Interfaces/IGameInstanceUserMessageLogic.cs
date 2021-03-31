using Core.Models;
using Core.Models.Views;
using System;
using System.Collections.Generic;

namespace Core.Logic.Interfaces
{
    public interface IGameInstanceUserMessageLogic
    {
        ICollection<GameInstanceUserMessage> Get(Guid gameInstanceId);
        ICollection<GameInstanceUserMessageViewResult> GetViewResults(Guid gameInstanceId);
        GameInstanceUserMessage Get(int id);
        GameInstanceUserMessage Save(GameInstanceUserMessage modelToSave);
        void Delete(int id);
    }
}
