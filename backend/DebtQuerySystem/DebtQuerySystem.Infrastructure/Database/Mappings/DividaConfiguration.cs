using DebtQuerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtQuerySystem.Infrastructure.Database.Mappings;

public class DividaConfiguration : IEntityTypeConfiguration<Divida>
{
    public void Configure(EntityTypeBuilder<Divida> builder)
    {
        builder.ToTable("Dividas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(p => p.Parcelas)
            .WithOne()
            .HasForeignKey(parcela => parcela.DividaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Parcelas)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
