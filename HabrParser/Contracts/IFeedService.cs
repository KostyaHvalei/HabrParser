namespace HabrParser.Contracts;

public interface IFeedService
{
    public Task<string> LoadPageAsync(int pageNumber);
}