using HabrParser.Models;
using Microsoft.EntityFrameworkCore;

namespace HabrParser.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<Article> Articles { get; set; }
}