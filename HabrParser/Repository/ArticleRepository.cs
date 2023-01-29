using HabrParser.Contracts;
using HabrParser.Data;
using HabrParser.Models;
using Microsoft.EntityFrameworkCore;

namespace HabrParser.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationContext _context;

    public ArticleRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllArticlesAsync() =>
        await _context.Articles
            .OrderByDescending(a => a.PublishedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<int> AddArticlesAsync(List<Article> articles)
    {
        await _context.Articles.AddRangeAsync(articles);
        return await _context.SaveChangesAsync();
    }

    public async Task<Article?> GetLastAsync()
    {
        return await _context.Articles.OrderBy(a => a.PublishedAt).LastOrDefaultAsync();
    }
}