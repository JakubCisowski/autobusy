using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class RealizacjaPrzejazdu
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int RealizacjaPrzejazduId { get; set; }
	
	public DateTime FaktycznaGodzina { get; set; }

	[ForeignKey("PlanKursuId")]
	public PlanKursu PlanKursu { get; set; }
	
	[ForeignKey("PrzejazdId")]
	public Przejazd Przejazd { get; set; }
}