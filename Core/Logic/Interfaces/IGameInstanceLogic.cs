using Core.Framework.Models;
using Core.Models;
using Core.Models.Dtos;
using System;
using System.Collections.Generic;

namespace Core.Logic.Interfaces
{
    public interface IGameInstanceLogic
    {
        ICollection<GameInstance> Get();
        ICollection<GameInstance> GetCurrentUserGameInstancesWithoutGameStates();
        GameInstance Get(Guid id);
        GameInstance Save(GameInstance modelToSave);
        void Delete(Guid id);
        IResponse<GameInstance> New(CreateGameInstanceDto dto);
        IResponse<GameInstance> Play(PlayGameInstanceDto dto);
    }
}
