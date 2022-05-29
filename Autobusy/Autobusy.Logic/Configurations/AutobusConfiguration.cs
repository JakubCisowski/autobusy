using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class AutobusConfiguration : IEntityTypeConfiguration<Autobus>
{
	public void Configure(EntityTypeBuilder<Autobus> builder)
	{
		builder.ToTable("Autobusy");

		builder.HasKey(a => a.Id);

		builder.Property(a => a.Id)
			.ValueGeneratedOnAdd();

		builder.Property(a => a.SpalanieNa100)
			.HasPrecision(4, 2);
	}
}