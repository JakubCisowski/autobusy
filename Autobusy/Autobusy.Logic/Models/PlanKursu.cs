using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class PlanKursu : IIdentifiable
{
	public DateTime PlanowaGodzina { get; set; }

	public int KursId { get; set; }
	public Kurs Kurs { get; set; }

	public int PrzystanekWLiniiId { get; set; }
	public PrzystanekWLinii PrzystanekWLinii { get; set; }

	public List<RealizacjaPrzejazdu> RealizacjePrzejazdu { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is PlanKursu planKursu && planKursu.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}