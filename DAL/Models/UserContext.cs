using System.Data.Entity;

namespace DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rpg"); // role-playing game
        }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Hero> Heroes { get; set; }
    }
}