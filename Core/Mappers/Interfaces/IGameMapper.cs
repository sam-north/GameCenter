﻿using Core.Models;
using Core.Models.Dtos;
using System.Collections.Generic;

namespace Core.Mappers.Interfaces
{
    public interface IGameMapper
    {
        GameDto Map(Game source);
        ICollection<GameDto> Map(ICollection<Game> source);
    }
}
