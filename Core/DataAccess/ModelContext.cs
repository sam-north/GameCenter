using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {

        }

        public DbSet<CRUDExample> CRUDExamples { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameInstance> GameInstances { get; set; }
        public DbSet<GameInstanceState> GameInstanceStates { get; set; }
        public DbSet<GameInstanceUser> GameInstanceUsers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
