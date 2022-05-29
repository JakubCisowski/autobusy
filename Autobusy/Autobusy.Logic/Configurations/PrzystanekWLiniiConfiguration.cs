using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class PrzystanekWLiniiConfiguration : IEntityTypeConfiguration<PrzystanekWLinii>
{
	public void Configure(EntityTypeBuilder<PrzystanekWLinii> builder)
	{
		builder.ToTable("PrzystankiWLinii");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedOnAdd();

		builder.HasOne(p => p.Przystanek)
			.WithMany(pr => pr.Przystanki)
			.HasForeignKey(p => p.PrzystanekId);

		builder.HasOne(p => p.Linia)
			.WithMany(l => l.PrzystankiWLinii)
			.HasForeignKey(p => p.LiniaId);
	}
}