using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Logic.Models;

public class Przystanek
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PrzystanekId { get; set; }
	
	public string Numer { get; set; }
	public string Nazwa { get; set; }
	public string Adres { get; set; }
	
	public List<PrzystanekWLinii> Przystanki { get; set; }
}