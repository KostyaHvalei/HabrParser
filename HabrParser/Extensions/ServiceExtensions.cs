using HabrParser.Contracts;
using HabrParser.Services;

namespace HabrParser.Extensions;

public static class ServiceExtensions
{
    public static void AddFeedHttpClient(this IServiceCollection services, IConfiguration conf)
    {
        services.AddHttpClient<IFeedService, FeedService>(client =>
        {
            var uriString = conf.GetSection("HabrFeedURL").Value;
            if (uriString != null)
                client.BaseAddress = new Uri(uriString);
        });
    }
    
    public static void AddFeedService(this IServiceCollection services)
    {
        services.AddTransient<IFeedService, FeedService>();
    }

    public static void AddArticlesService(this IServiceCollection services)
    {
        services.AddSingleton<IArticlesService, ArticlesService>();
    }
}