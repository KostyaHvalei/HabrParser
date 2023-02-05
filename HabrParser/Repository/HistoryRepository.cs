using HabrParser.Contracts;
using HabrParser.Data;
using HabrParser.Models;
using Microsoft.EntityFrameworkCore;

namespace HabrParser.Repository;

public class HistoryRepository : IHistoryRepository
{
    private readonly ApplicationContext _context;

    public HistoryRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<LoadInfo>> GetFullHistoryAsync()
    {
        return await _context.History
            .OrderByDescending(loadInfo => loadInfo.LoadedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(LoadInfo loadInfo)
    {
        await _context.History.AddAsync(loadInfo);
        await _context.SaveChangesAsync();
    }
}