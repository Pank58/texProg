using Microsoft.EntityFrameworkCore;
using Magazine.Core.Models; 

namespace Magazine.WebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Product> Products { get; set; }
}