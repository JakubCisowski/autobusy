using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class PrzejazdConfiguration : IEntityTypeConfiguration<Przejazd>
{
	public void Configure(EntityTypeBuilder<Przejazd> builder)
	{
		builder.ToTable("Przejazdy");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedOnAdd();

		builder.Property(p => p.IloscSpalonegoPaliwa)
			.HasPrecision(4, 2);

		builder.HasOne(p => p.Autobus)
			.WithMany(a => a.Przejazdy)
			.HasForeignKey(p => p.AutobusId);

		builder.HasOne(p => p.Kierowca)
			.WithMany(k => k.Przejazdy)
			.HasForeignKey(p => p.KierowcaId);

		builder.HasOne(p => p.Kurs)
			.WithMany(k => k.Przejazdy)
			.HasForeignKey(p => p.KursId);
	}
}