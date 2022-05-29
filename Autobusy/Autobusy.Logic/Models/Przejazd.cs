using Autobusy.Logic.Interfaces;

namespace Autobusy.Logic.Models;

public class Przejazd : IIdentifiable
{
	public double IloscSpalonegoPaliwa { get; set; }
	public int IloscSkasowanychBiletow { get; set; }
	public DateTime Data { get; set; }

	public int KursId { get; set; }
	public Kurs Kurs { get; set; }

	public int KierowcaId { get; set; }
	public Kierowca Kierowca { get; set; }

	public int AutobusId { get; set; }
	public Autobus Autobus { get; set; }

	public List<RealizacjaPrzejazdu> RealizacjePrzejazdu { get; set; }
	public int Id { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Przejazd przejazd && przejazd.Id == this.Id)
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