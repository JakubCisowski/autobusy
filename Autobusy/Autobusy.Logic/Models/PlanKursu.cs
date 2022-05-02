using System;
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
	
	public int PrzystanekWLiniiId { get; set; }	// W przypadku relacji 1:1, jedna z encji musi posiadać nie tylko obiekt ale również id drugiego.
	public PrzystanekWLinii PrzystanekWLinii { get; set; }
	
	public List<RealizacjaPrzejazdu> RealizacjePrzejazdu { get; set; }
}