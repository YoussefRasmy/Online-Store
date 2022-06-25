namespace Day2Task.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(Guid id);
        bool SaveChanges();
    }
}
