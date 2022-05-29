using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Kurs : IIdentifiable
{
	public DzienTygodnia DzienTygodnia { get; set; }
	public DateTime GodzinaRozpoczecia { get; set; }

	public int LiniaId { get; set; }
	public Linia Linia { get; set; }

	public List<Przejazd> Przejazdy { get; set; }
	public List<PlanKursu> PlanyKursu { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Kurs kurs && kurs.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}