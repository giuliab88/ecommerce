namespace SampleCommerce.Repositories
{
    public interface IRepoRead<TKey, T>
    {
        T? GetById(TKey id);
        List<T> GetAll();
    }
}
