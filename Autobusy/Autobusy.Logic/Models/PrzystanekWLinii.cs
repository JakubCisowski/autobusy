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
	
	public List<PlanKursu> PlanyKursu { get; set; }
	
	public override bool Equals(object obj)
	{
		if (obj is PrzystanekWLinii przystanekWLinii && przystanekWLinii.PrzystanekWLiniiId == this.PrzystanekWLiniiId)
		{
			return true;
		}

		return false;
	}
}