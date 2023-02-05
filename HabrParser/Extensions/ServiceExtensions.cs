using HabrParser.Contracts;
using HabrParser.Data;
using HabrParser.Repository;
using HabrParser.Services;
using Hangfire;
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
        services.AddScoped<IHistoryRepository, HistoryRepository>();
    }

    public static void AddArticlesService(this IServiceCollection services)
    {
        services.AddScoped<IArticlesService, ArticlesService>();
    }

    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(conf => conf
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();
    }
    
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
}