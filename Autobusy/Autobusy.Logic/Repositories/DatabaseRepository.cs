using Autobusy.Logic.Contexts;
using Autobusy.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autobusy.Logic.Repositories;

public class DatabaseRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class, IIdentifiable
{
	private readonly AutobusyContext _dbContext;

	public DatabaseRepository(AutobusyContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Dispose()
	{
		_dbContext?.Dispose();
	}

	public TEntity GetById(int id)
	{
		return _dbContext.Set<TEntity>().Find(id);
	}

	public TEntity GetFirst(Func<TEntity, bool> predicate)
	{
		return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
	}

	public List<TEntity> List()
	{
		return _dbContext.Set<TEntity>().ToList();
	}

	public List<TEntity> List(Func<TEntity, bool> predicate)
	{
		return _dbContext.Set<TEntity>().Where(predicate).ToList();
	}

	public void Add(TEntity entity)
	{
		_dbContext.Set<TEntity>().Add(entity);

		_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Vehicles ON");
		_dbContext.SaveChanges();
		_dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Vehicles OFF");
	}

	public void Delete(TEntity entity)
	{
		TEntity entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

		_dbContext.Set<TEntity>().Remove(entityFromDb);

		_dbContext.SaveChanges();
	}

	public void Update(TEntity entity)
	{
		TEntity entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

		_dbContext.Entry(entityFromDb).CurrentValues.SetValues(entity);

		_dbContext.SaveChanges();
	}

	public void UpdateMany(IEnumerable<TEntity> entities)
	{
		foreach (TEntity entity in entities)
		{
			TEntity entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

			_dbContext.Entry(entityFromDb).CurrentValues.SetValues(entity);
		}

		_dbContext.SaveChanges();
	}
}