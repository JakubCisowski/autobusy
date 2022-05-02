using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Kierowca
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int KierowcaId { get; set; }
	
	public string Imie { get; set; }
	public string Nazwisko { get; set; }
	public int Doswiadczenie{ get; set; }
	
	public List<Przejazd> Przejazdy { get; set; }
}