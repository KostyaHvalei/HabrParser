namespace HabrParser.Contracts;

public interface IFeedService
{
    public Task<string> LoadPage(int pageNumber);
}