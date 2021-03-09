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
        Response<GameInstance> New(CreateGameInstanceDto dto);
        Response<GameInstance> Play(PlayGameInstanceDto dto);
    }
}
