using Day2Task.Models;
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Context;

namespace Day2Task.Data;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
	where TEntity : class
{
	private readonly OnlineStoreContext _context;

	public GenericRepository(OnlineStoreContext context)
	{
		_context = context;
	}
	public void Add(TEntity entity)
	{
		_context.Set<TEntity>().Add(entity);
		SaveChanges();
	}

	public void Delete(Guid id)
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

	public TEntity GetById(Guid id)
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

