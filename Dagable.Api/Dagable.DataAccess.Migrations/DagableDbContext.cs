using Dagable.DataAccess.Migrations.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Dagable.DataAccess.Migrations
{
    public class DagableDbContext : DbContext
    {
        public DbSet<Graph> Graphs { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        public DagableDbContext() : base()
        {
        }

        public DagableDbContext (DbContextOptions<DagableDbContext> options)
        : base(options)
        {
        }
    }
}