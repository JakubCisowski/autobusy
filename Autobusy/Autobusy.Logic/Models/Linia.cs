using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Linia : IIdentifiable
{
	public string Numer { get; set; }
	public TypLinii Typ { get; set; }
	public double DlugoscWKm { get; set; }

	public List<PrzystanekWLinii> PrzystankiWLinii { get; set; }
	public List<Kurs> Kursy { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Linia linia && linia.Id == this.Id)
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