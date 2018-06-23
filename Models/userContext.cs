using Microsoft.EntityFrameworkCore;
 
namespace DojoActivityCenter.Models
{
    public class userContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
     
        public DbSet<User> users {get; set;}
        public DbSet<Activity> activities {get;set;} 
        public DbSet<Guest> guests {get;set;} 
        public userContext(DbContextOptions<userContext> options) : base(options) { }
    }
}