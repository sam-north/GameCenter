using Core.Models;
using Core.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {

        }

        #region tables
        public DbSet<CRUDExample> CRUDExamples { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameInstance> GameInstances { get; set; }
        public DbSet<GameInstanceState> GameInstanceStates { get; set; }
        public DbSet<GameInstanceStateHistory> GameInstanceStateHistories { get; set; }
        public DbSet<GameInstanceUser> GameInstanceUsers { get; set; }
        public DbSet<GameInstanceUserMessage> GameInstanceUserMessages { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region views
        public DbSet<GameInstanceUserMessageViewResult> GameInstanceUserMessageViewResults { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
