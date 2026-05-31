namespace SampleCommerce.Repositories
{
    public interface IRepoWrite<T>
    {
        bool Create(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }
}
