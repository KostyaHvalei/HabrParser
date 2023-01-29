using HabrParser.Models;

namespace HabrParser.Contracts;

public interface IHistoryRepository
{
    public Task<IEnumerable<LoadInfo>> GetFullHistory();
    public Task Add(LoadInfo loadInfo);
}