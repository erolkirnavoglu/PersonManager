using Microsoft.EntityFrameworkCore;
using PersonManager.Domain;

namespace PersonManager.Persistence.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
    }
}
