using HabrParser.Contracts;
using HabrParser.Data;
using HabrParser.Repository;
using HabrParser.Services;
using Microsoft.EntityFrameworkCore;

namespace HabrParser.Extensions;

public static class ServiceExtensions
{
    public static void AddFeedHttpClient(this IServiceCollection services, IConfiguration conf)
    {
        services.AddHttpClient<IFeedService, FeedService>(client =>
            client.BaseAddress = new Uri(conf.GetSection("HabrFeedURL").Value)
        );
    }

    public static void ConfigureApplicationContext(this IServiceCollection services, IConfiguration conf)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(conf.GetConnectionString("DefaultConnection"), b =>
            b.MigrationsAssembly("HabrParser")));
    }
    
    public static void AddFeedParsingService(this IServiceCollection services)
    {
        services.AddSingleton<IFeedParsingService, FeedParsingService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArticleRepository, ArticleRepository>();
    }
}