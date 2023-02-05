using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IFeedParsingService
{
    public Task<List<Article>> ParseRSSPageAsync(string rssContent);
}