using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Models;

public class Kurs
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int KursId { get; set; }
	
	public DateTime DzienOdbycia { get; set; }
	
	public List<Przejazd> Przejazdy { get; set; }
	public List<PlanKursu> PlanyKursu { get; set; }
}