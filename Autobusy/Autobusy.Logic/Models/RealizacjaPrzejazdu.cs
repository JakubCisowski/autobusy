using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class RealizacjaPrzejazdu
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int RealizacjaPrzejazduId { get; set; }
	
	public DateTime FaktycznaGodzina { get; set; }
	
	public int PlanKursuId { get; set; }	// W przypadku relacji 1:1, jedna z encji musi posiadać nie tylko obiekt ale również id drugiego.
	public PlanKursu PlanKursu { get; set; }
}