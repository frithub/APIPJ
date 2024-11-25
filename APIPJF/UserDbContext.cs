namespace APIPJF;
using Microsoft.EntityFrameworkCore;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // DB Connection
        optionsBuilder.UseSqlServer(@"Server=localhost;User ID=SA;Password=Password123!;TrustServerCertificate=True;Database=userdb");
        
    }
  

    
}