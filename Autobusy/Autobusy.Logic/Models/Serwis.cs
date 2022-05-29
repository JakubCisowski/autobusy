using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Serwis : IIdentifiable
{
	public DateTime Data { get; set; }
	public TypSerwisu Typ { get; set; }
	public decimal Cena { get; set; }
	public string Opis { get; set; }

	public int AutobusId { get; set; }
	public Autobus NaprawianyAutobus { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Serwis serwis && serwis.Id == Id) return true;

		return false;
	}

	public override int GetHashCode()
	{
		return Id;
	}
}