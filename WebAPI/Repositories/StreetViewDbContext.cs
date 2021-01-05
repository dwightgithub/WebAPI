using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class StreetViewDbContext:DbContext
    {
        private IConfiguration Configuration { get; }

        public StreetViewDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("testdb"));
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ImageInfoEntity> ImageInfos { get; set; }
        public DbSet<RateEntity> Rates { get; set; }
        


    }
}

