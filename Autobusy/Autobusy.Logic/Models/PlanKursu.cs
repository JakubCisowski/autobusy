using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class PlanKursu
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PlanKursuId { get; set; }
	
	public DateTime PlanowaGodzina { get; set; }
	
	[ForeignKey("KursId")]
	public Kurs Kurs { get; set; }
	
	[ForeignKey("PrzystanekId")]
	public Przystanek Przystanek { get; set; }
	
	[ForeignKey("RealizacjaPrzejazduId")]
	public RealizacjaPrzejazdu RealizacjaPrzejazdu { get; set; }
}