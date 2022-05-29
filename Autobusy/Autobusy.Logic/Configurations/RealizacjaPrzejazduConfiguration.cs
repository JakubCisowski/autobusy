using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class RealizacjaPrzejazduConfiguration : IEntityTypeConfiguration<RealizacjaPrzejazdu>
{
	public void Configure(EntityTypeBuilder<RealizacjaPrzejazdu> builder)
	{
		builder.ToTable("RealizacjePrzejazdu");

		builder.HasKey(rp => rp.Id);

		builder.Property(rp => rp.Id)
			.ValueGeneratedOnAdd();

		builder.HasOne(rp => rp.PlanKursu)
			.WithMany(pk => pk.RealizacjePrzejazdu)
			.HasForeignKey(rp => rp.PlanKursuId);

		builder.HasOne(rp => rp.Przejazd)
			.WithMany(p => p.RealizacjePrzejazdu)
			.HasForeignKey(rp => rp.PrzejazdId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}