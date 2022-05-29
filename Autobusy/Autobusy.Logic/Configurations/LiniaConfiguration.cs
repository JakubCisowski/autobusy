using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class LiniaConfiguration : IEntityTypeConfiguration<Linia>
{
	public void Configure(EntityTypeBuilder<Linia> builder)
	{
		builder.ToTable("Linie");

		builder.HasKey(l => l.Id);

		builder.Property(l => l.Id)
			.ValueGeneratedOnAdd();

		builder.Property(l => l.DlugoscWKm)
			.HasPrecision(4, 2);
	}
}