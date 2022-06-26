
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Context;


namespace OnlineStoreBack_API.Repository;

public class GenericRepository<TEntity, TID> : IGenericRepository<TEntity, TID>
	where TEntity : class
{
	protected readonly OnlineStoreContext _context;

	public GenericRepository(OnlineStoreContext context)
	{
		_context = context;
	}
	public void Add(TEntity entity)
	{
		_context.Set<TEntity>().Add(entity);
		SaveChanges();
	}

	public void Delete(TID id)
	{
		var entityToDelete = GetById(id);
		if (entityToDelete is not null)
		{
			_context.Set<TEntity>().Remove(entityToDelete);
		}
		SaveChanges();
	}

	public List<TEntity> GetAll()
	{
		return _context.Set<TEntity>().ToList();
	}

	public TEntity GetById(TID id)
	{
		return _context.Set<TEntity>().Find(id);
	}

	public bool SaveChanges()
	{
		return _context.SaveChanges() > 0;
	}

	public void Update(TEntity entity)
	{

	}
}

