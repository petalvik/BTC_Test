using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Entities;

namespace DAL.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base("DefaultConnection") { }
        
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Hero> Heroes { get; set; }
    }
}