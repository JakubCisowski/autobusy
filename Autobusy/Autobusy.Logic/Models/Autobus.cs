using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Autobus : IIdentifiable
{
	public string Marka { get; set; }
	public string NumerRejestracyjny { get; set; }
	public DateTime DataProdukcji { get; set; }
	public StanAutobusu StanAutobusu { get; set; }
	public double SpalanieNa100 { get; set; }

	public List<Serwis> Serwisy { get; set; }

	public List<Przejazd> Przejazdy { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Autobus autobus && autobus.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}