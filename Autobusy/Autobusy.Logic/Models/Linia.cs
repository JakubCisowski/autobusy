using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Linia
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int LiniaId { get; set; }
	
	public string Numer { get; set; }
	public TypLinii Typ { get; set; }
	public double DlugoscWKm { get; set; }
	
	public List<PrzystanekWLinii> Przystanki { get; set; }
	public List<Kurs> Kursy { get; set; }
}