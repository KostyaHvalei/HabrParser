using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IHistoryRepository
{
    public Task<IEnumerable<LoadInfo>> GetFullHistoryAsync();
    public Task AddAsync(LoadInfo loadInfo);
}