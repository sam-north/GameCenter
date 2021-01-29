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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
