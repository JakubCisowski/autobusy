using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Kierowca : IIdentifiable
{
	public string Imie { get; set; }
	public string Nazwisko { get; set; }
	public int Doswiadczenie { get; set; }

	public List<Przejazd> Przejazdy { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Kierowca kierowca && kierowca.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}