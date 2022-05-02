using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class PrzystanekWLinii
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PrzystanekWLiniiId { get; set; }
	
	public int LiczbaPorzadkowa { get; set; }
	
	[ForeignKey("PrzystanekId")]
	public Przystanek Przystanek { get; set; }
	
	[ForeignKey("LiniaId")]
	public Linia Linia { get; set; }
	
	[ForeignKey("PlanKursuId")]
	public PlanKursu PlanKursu { get; set; }
}