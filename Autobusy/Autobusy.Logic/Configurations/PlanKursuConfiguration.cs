using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class PlanKursuConfiguration : IEntityTypeConfiguration<PlanKursu>
{
	public void Configure(EntityTypeBuilder<PlanKursu> builder)
	{
		builder.ToTable("PlanyKursu");

		builder.HasKey(pk => pk.Id);

		builder.Property(pk => pk.Id)
			.ValueGeneratedOnAdd();

		builder.HasOne(pk => pk.Kurs)
			.WithMany(k => k.PlanyKursu)
			.HasForeignKey(pk => pk.KursId);

		builder.HasOne(pk => pk.PrzystanekWLinii)
			.WithMany(p => p.PlanyKursu)
			.HasForeignKey(pk => pk.PrzystanekWLiniiId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}