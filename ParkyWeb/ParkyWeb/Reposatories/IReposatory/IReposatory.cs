namespace ParkyWeb.Reposatories.IReposatory
{
    public interface IReposatory<T> where T: class
    {
        Task<bool> CreateAsync(string url, T item);
        Task<bool> UpdateAsync(string url, T item);
        Task<bool> DeleteAsync(string url,int id);
        Task <T> GetAsync(string url, int id);
        Task<IEnumerable<T>> GetAllAsync(string url);

    }
}
