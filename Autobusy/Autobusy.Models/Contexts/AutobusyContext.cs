using Microsoft.EntityFrameworkCore;

namespace Autobusy.Models.Contexts;

public class AutobusyContext : DbContext
{
	public DbSet<Autobus> Autobusy { get; set; }
	public DbSet<Kierowca> Kierowcy { get; set; }
	public DbSet<Kurs> Kursy { get; set; }
	public DbSet<Linia> Linie { get; set; }
	public DbSet<PlanKursu> PlanyKursu { get; set; }
	public DbSet<Przejazd> Przejazdy { get; set; }
	public DbSet<Przystanek> Przystanki { get; set; }
	public DbSet<PrzystanekWLinii> PrzystankiWLinii { get; set; }
	public DbSet<RealizacjaPrzejazdu> RealizacjePrzejazdu { get; set; }
	public DbSet<Serwis> Serwisy { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(ConnectionStringManager.ConnectionString);
	}
}