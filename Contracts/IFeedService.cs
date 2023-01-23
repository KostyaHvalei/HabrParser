namespace Contracts;

public interface IFeedService
{
    public Task<string> Load(int pageNumber);
}