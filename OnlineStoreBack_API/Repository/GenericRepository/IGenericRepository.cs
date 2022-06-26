namespace OnlineStoreBack_API.Repository
{
    public interface IGenericRepository<TEntity,TID> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(TID id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(TID id);
        bool SaveChanges();
    }
}
