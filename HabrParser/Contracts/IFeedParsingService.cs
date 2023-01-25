using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IFeedParsingService
{
    public Task<List<Article>> ParseRSSPage(string rssContent);
}