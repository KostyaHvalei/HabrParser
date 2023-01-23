using Contracts;
using HabrParser.Services;

namespace HabrParser.Extensions;

public static class ServiceExtensions
{
    public static void AddFeedHttpClient(this IServiceCollection services, IConfiguration conf)
    {
        services.AddHttpClient<IFeedService, FeedService>(client =>
        {
            client.BaseAddress = new Uri(conf.GetSection("HabrFeedURL").Value);
        });
    }
}