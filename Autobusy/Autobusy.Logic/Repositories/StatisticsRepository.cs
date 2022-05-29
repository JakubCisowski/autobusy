using Autobusy.Logic.Contexts;
using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Repositories;

public class StatisticsRepository : IDisposable, IStatisticsRepository
{
	private readonly AutobusyContext _dbContext;

	public StatisticsRepository(AutobusyContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Dispose()
	{
		_dbContext?.Dispose();
	}

	public IEnumerable<(string, int)> GetDatabaseObjectsStatistics()
	{
		yield return ("Autobusy", _dbContext.Autobusy.Count());
		yield return ("Kierowcy", _dbContext.Kierowcy.Count());
		yield return ("Kursy", _dbContext.Kursy.Count());
		yield return ("Linie", _dbContext.Linie.Count());
		yield return ("Przejazdy", _dbContext.Przejazdy.Count());
		yield return ("Realizacje przejazdu", _dbContext.RealizacjePrzejazdu.Count());
		yield return ("Plany kursów", _dbContext.PlanyKursu.Count());
		yield return ("Przystanki", _dbContext.Przystanki.Count());
		yield return ("Przystanki w linii", _dbContext.PrzystankiWLinii.Count());
		yield return ("Serwisy", _dbContext.Serwisy.Count());
	}
}