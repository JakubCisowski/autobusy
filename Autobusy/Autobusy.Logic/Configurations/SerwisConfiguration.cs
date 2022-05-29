using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class SerwisConfiguration : IEntityTypeConfiguration<Serwis>
{
	public void Configure(EntityTypeBuilder<Serwis> builder)
	{
		builder.ToTable("Serwisy");

		builder.HasKey(s => s.Id);

		builder.Property(s => s.Id)
			.ValueGeneratedOnAdd();

		builder.Property(s => s.Cena)
			.HasPrecision(10, 2);

		builder.HasOne(s => s.NaprawianyAutobus)
			.WithMany(a => a.Serwisy)
			.HasForeignKey(s => s.AutobusId);
	}
}