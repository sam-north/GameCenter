using Core.DataAccess;
using Core.Logic.Interfaces;
using Core.Models;
using Core.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class GameInstanceUserMessageLogic : IGameInstanceUserMessageLogic
    {
        private ModelContext ModelContext { get; }
        public GameInstanceUserMessageLogic(ModelContext modelContext)
        {
            ModelContext = modelContext;
        }

        public ICollection<GameInstanceUserMessage> Get(Guid gameInstanceId)
        {
            var results = new List<GameInstanceUserMessage>();
            results = ModelContext.GameInstanceUserMessages.Where(x => x.GameInstanceId == gameInstanceId && !x.DateDeleted.HasValue).ToList();
            return results;
        }

        public ICollection<GameInstanceUserMessageViewResult> GetViewResults(Guid gameInstanceId)
        {
            var results = new List<GameInstanceUserMessageViewResult>();
            results = ModelContext.GameInstanceUserMessageViewResults.Where(x => x.GameInstanceId == gameInstanceId && !x.DateDeleted.HasValue).ToList();
            return results;
        }

        public GameInstanceUserMessage Get(int id)
        {
            return ModelContext.GameInstanceUserMessages.Single(x => x.Id == id);
        }

        public GameInstanceUserMessage Save(GameInstanceUserMessage modelToSave)
        {
            var entity = ModelContext.GameInstanceUserMessages.SingleOrDefault(x => x.Id == modelToSave.Id);

            if (entity == null)
            {
                entity = new GameInstanceUserMessage();
                entity.DateCreated = DateTimeOffset.Now;
                entity.UserId = modelToSave.UserId;
                entity.GameInstanceId = modelToSave.GameInstanceId;

                ModelContext.GameInstanceUserMessages.Add(entity);
            }

            entity.Text = modelToSave.Text;

            ModelContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            entity.DateDeleted = DateTimeOffset.Now;
            ModelContext.SaveChanges();
        }
    }
}
