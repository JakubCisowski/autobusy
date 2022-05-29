namespace Autobusy.Logic.Interfaces;

public interface IGenericRepository<TEntity>
{
	TEntity GetById(int id);

	TEntity GetFirst(Func<TEntity, bool> predicate);

	List<TEntity> List();

	List<TEntity> List(Func<TEntity, bool> predicate);

	void Add(TEntity entity);

	void Delete(TEntity entity);

	void Update(TEntity entity);

	void UpdateMany(IEnumerable<TEntity> entities);
}