using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Autobus
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int AutobusId { get; set; }
	
	public string Marka { get; set; }
	public string NumerRejestracyjny { get; set; }
	public DateTime DataProdukcji { get; set; }
	public StanAutobusu StanAutobusu { get; set; }
	public double SpalanieNa100 { get; set; }
	
	public List<Serwis> Serwisy { get; set; }
	
	public List<Przejazd> Przejazdy { get; set; }

	public override bool Equals(object obj)
	{
		if (obj is Autobus autobus && autobus.AutobusId == this.AutobusId)
		{
			return true;
		}

		return false;
	}
}