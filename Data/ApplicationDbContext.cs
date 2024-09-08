using Microsoft.EntityFrameworkCore;
using OfficeMangement.Models.Entities;

namespace OfficeMangement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<StudentEntity> Students { get; set; }
    }
}
