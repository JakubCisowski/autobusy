namespace Autobusy.Logic.Interfaces;

public interface IStatisticsRepository
{
	IEnumerable<(string, int)> GetDatabaseObjectsStatistics();
}