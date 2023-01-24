﻿using HabrParser.Contracts;
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

    public async Task<int> AddArticles(List<Article> articles)
    {
        await _context.Articles.AddRangeAsync(articles);
        return await _context.SaveChangesAsync();
    }
}