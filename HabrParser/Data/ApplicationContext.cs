using HabrParser.Models;
using Microsoft.EntityFrameworkCore;

namespace HabrParser.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Article> Articles { get; set; }
    public DbSet<LoadInfo> History { get; set; }
}