using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class KierowcaConfiguration : IEntityTypeConfiguration<Kierowca>
{
	public void Configure(EntityTypeBuilder<Kierowca> builder)
	{
		builder.ToTable("Kierowcy");

		builder.HasKey(k => k.Id);

		builder.Property(k => k.Id)
			.ValueGeneratedOnAdd();
	}
}