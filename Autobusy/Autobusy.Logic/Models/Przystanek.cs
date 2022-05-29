using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Przystanek : IIdentifiable
{
	public string Numer { get; set; }
	public string Nazwa { get; set; }
	public string Adres { get; set; }

	public List<PrzystanekWLinii> Przystanki { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Przystanek przystanek && przystanek.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}