using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class KursConfiguration : IEntityTypeConfiguration<Kurs>
{
	public void Configure(EntityTypeBuilder<Kurs> builder)
	{
		builder.ToTable("Kursy");

		builder.HasKey(k => k.Id);

		builder.Property(k => k.Id)
			.ValueGeneratedOnAdd();

		builder.HasOne(k => k.Linia)
			.WithMany(l => l.Kursy)
			.HasForeignKey(k => k.LiniaId);
	}
}