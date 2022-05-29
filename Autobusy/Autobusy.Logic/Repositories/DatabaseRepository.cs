using System.Linq.Expressions;
using Autobusy.Logic.Contexts;
using Autobusy.Logic.Helpers;
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

	public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes)
	{
		var query = _dbContext.Set<TEntity>().AsQueryable();

		query = includes.Aggregate(query, (resultSoFar, next) => resultSoFar.Include(next));

		return query.FirstOrDefault(x => x.Id == id);
	}

	public TEntity GetFirst(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
	{
		var query = _dbContext.Set<TEntity>().AsQueryable();

		query = includes.Aggregate(query, (resultSoFar, next) => resultSoFar.Include(next));

		return query.FirstOrDefault(predicate);
	}

	public List<TEntity> List(params Expression<Func<TEntity, object>>[] includes)
	{
		var query = _dbContext.Set<TEntity>().AsQueryable();

		query = includes.Aggregate(query, (resultSoFar, next) => resultSoFar.Include(next));

		return query.ToList();
	}

	public List<TEntity> List(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes)
	{
		var query = _dbContext.Set<TEntity>().AsQueryable();

		query = includes.Aggregate(query, (resultSoFar, next) => resultSoFar.Include(next));

		return query.AsEnumerable().Where(predicate).ToList();
	}

	public void Add(TEntity entity)
	{
		_dbContext.Set<TEntity>().Add(entity);

		var tableName = TableNameHelper.GetTableName(entity);

		// Tymczasowo ustawiamy IDENTITY_INSERT na ON żeby łatwo wstawić rekord z kluczem obcym pobranym już wcześniej z bazy danych
		_dbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON");
		_dbContext.SaveChanges();
		_dbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} OFF");
	}

	public void Delete(TEntity entity)
	{
		var entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

		_dbContext.Set<TEntity>().Remove(entityFromDb);

		_dbContext.SaveChanges();
	}

	public void Update(TEntity entity)
	{
		var entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

		if (entityFromDb is null)
		{
			Add(entity);
		}
		else
		{
			_dbContext.Entry(entityFromDb).CurrentValues.SetValues(entity);
		}

		_dbContext.SaveChanges();
	}

	public void UpdateMany(IEnumerable<TEntity> entities)
	{
		if (entities is null)
		{
			return;
		}
		
		foreach (var entity in entities)
		{
			var entityFromDb = _dbContext.Set<TEntity>().Find(entity.Id);

			if (entityFromDb is null)
			{
				Add(entity);
			}
			else
			{
				_dbContext.Entry(entityFromDb).CurrentValues.SetValues(entity);
			}
		}

		_dbContext.SaveChanges();
	}

	public void ExecuteSqlQuery(string query)
	{
		_dbContext.Database.ExecuteSqlRaw(query);
		_dbContext.SaveChanges();
	}

	public void SaveChanges()
	{
		_dbContext.SaveChanges();
	}
}