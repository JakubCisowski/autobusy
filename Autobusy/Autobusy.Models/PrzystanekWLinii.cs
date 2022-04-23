using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobusy.Models;

public class PrzystanekWLinii
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PrzystanekWLiniiId { get; set; }
	
	[ForeignKey("PrzystanekId")]
	public Przystanek Przystanek { get; set; }
	
	[ForeignKey("LiniaId")]
	public Linia Linia { get; set; }
}