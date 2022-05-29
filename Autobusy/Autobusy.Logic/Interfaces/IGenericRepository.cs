using System.Linq.Expressions;

namespace Autobusy.Logic.Interfaces;

public interface IGenericRepository<TEntity>
{
	TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes);

	TEntity GetFirst(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

	List<TEntity> List(params Expression<Func<TEntity, object>>[] includes);

	List<TEntity> List(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includes);

	void Add(TEntity entity);

	void Delete(TEntity entity);

	void Update(TEntity entity);

	void UpdateMany(IEnumerable<TEntity> entities);
}