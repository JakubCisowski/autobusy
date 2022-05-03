using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Kurs
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int KursId { get; set; }
	
	public DzienTygodnia DzienTygodnia { get; set; }
	public DateTime GodzinaRozpoczecia { get; set; }
	
	[ForeignKey("LiniaId")]
	public Linia Linia { get; set; }
	
	public List<Przejazd> Przejazdy { get; set; }
	public List<PlanKursu> PlanyKursu { get; set; }
	
	public override bool Equals(object obj)
	{
		if (obj is Kurs kurs && kurs.KursId == this.KursId)
		{
			return true;
		}

		return false;
	}
}