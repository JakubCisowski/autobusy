using Autobusy.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autobusy.Logic.Configurations;

public class PrzystanekConfiguration : IEntityTypeConfiguration<Przystanek>
{
	public void Configure(EntityTypeBuilder<Przystanek> builder)
	{
		builder.ToTable("Przystanki");

		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
			.ValueGeneratedOnAdd();
	}
}