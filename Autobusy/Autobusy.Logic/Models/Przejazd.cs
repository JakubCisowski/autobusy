using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Przejazd
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PrzejazdId { get; set; }
	
	public double IloscSpalonegoPaliwa{ get; set; }
	public int IloscSkasowanychBiletow { get; set; }
	
	[ForeignKey("KursId")]
	public Kurs Kurs { get; set; }
	
	[ForeignKey("KierowcaId")]
	public Kierowca Kierowca { get; set; }
	
	[ForeignKey("AutobusId")]
	public Autobus Autobus { get; set; }
	
	public List<RealizacjaPrzejazdu> Realizacje { get; set; }
}