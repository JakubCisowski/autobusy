using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class PrzystanekWLinii : IIdentifiable
{
	public int LiczbaPorzadkowa { get; set; }

	public int PrzystanekId { get; set; }
	public Przystanek Przystanek { get; set; }

	public int LiniaId { get; set; }
	public Linia Linia { get; set; }

	public List<PlanKursu> PlanyKursu { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is PrzystanekWLinii przystanekWLinii && przystanekWLinii.Id == this.Id)
		{
			return true;
		}

		return false;
	}

	public override int GetHashCode()
	{
		return this.Id;
	}
}