using Autobusy.Logic.Models;

namespace Autobusy.Logic.Helpers;

public static class TableNameHelper
{
	public static string GetTableName<TEntity>(TEntity entity)
	{
		var prefix = "dbo.";
		var suffix = "";

		switch (entity)
		{
			case Autobus:
				suffix = "Autobusy";
				break;

			case Kierowca:
				suffix = "Kierowcy";
				break;

			case Kurs:
				suffix = "Kursy";
				break;

			case Linia:
				suffix = "Linie";
				break;

			case PlanKursu:
				suffix = "PlanyKursu";
				break;

			case Przejazd:
				suffix = "Przejazdy";
				break;

			case Przystanek:
				suffix = "Przystanki";
				break;

			case PrzystanekWLinii:
				suffix = "PrzystankiWLinii";
				break;

			case RealizacjaPrzejazdu:
				suffix = "RealizacjePrzejazdu";
				break;

			case Serwis:
				suffix = "Serwisy";
				break;
		}

		return prefix + suffix;
	}
}