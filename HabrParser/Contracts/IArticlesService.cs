using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IArticlesService
{
    public Task<List<Article>> ParsePage(int pageNumber);
}