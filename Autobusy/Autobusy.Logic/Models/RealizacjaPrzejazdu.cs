using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class RealizacjaPrzejazdu : IIdentifiable
{
	public DateTime FaktycznaGodzina { get; set; }

	public int PlanKursuId { get; set; }
	public PlanKursu PlanKursu { get; set; }

	public int PrzejazdId { get; set; }
	public Przejazd Przejazd { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is RealizacjaPrzejazdu realizacjaPrzejazdu && realizacjaPrzejazdu.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}