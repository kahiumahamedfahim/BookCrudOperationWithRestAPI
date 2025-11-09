using Microsoft.EntityFrameworkCore;
using BookCrudOperationWIthRestAPI.Entity;
namespace BookCrudOperationWIthRestAPI.DataAcess
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
       public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
