using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IArticleRepository
{
    public Task<IEnumerable<Article>> GetAllArticlesAsync();
    public Task<int> AddArticles(List<Article> articles);
    public Task<Article?> GetLast();
}