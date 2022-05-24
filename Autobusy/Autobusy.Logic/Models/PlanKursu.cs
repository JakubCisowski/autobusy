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
	
	[ForeignKey("PrzystanekWLiniiId")]
	public PrzystanekWLinii PrzystanekWLinii { get; set; }
	
	public List<RealizacjaPrzejazdu> RealizacjePrzejazdu { get; set; }
	
	public override bool Equals(object obj)
	{
		if (obj is PlanKursu planKursu && planKursu.PlanKursuId == this.PlanKursuId)
		{
			return true;
		}

		return false;
	}
}