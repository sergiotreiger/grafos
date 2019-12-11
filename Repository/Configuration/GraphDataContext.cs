using Microsoft.EntityFrameworkCore;
using Grafos.Models;

namespace Grafos.Repository.Configuration
{
    public class GraphDataContext : DbContext
    {
        public GraphDataContext(DbContextOptions<GraphDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Graph>().ToTable("Graph");
        }

        public DbSet<Graph> Graph { get; set; }
    }
}
