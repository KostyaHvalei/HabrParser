namespace HabrParser.Contracts;

public interface IArticlesService
{
    public Task<int> LoadNewArticlesAsync();
}