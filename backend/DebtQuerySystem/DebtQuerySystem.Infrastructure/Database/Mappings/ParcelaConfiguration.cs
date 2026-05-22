using DebtQuerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtQuerySystem.Infrastructure.Database.Mappings;

public class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
{
    public void Configure(EntityTypeBuilder<Parcela> builder)
    {
        builder.ToTable("Parcelas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ParcelaNumero)
            .IsRequired();

        builder.Property(p => p.ValorOriginal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.TaxaAdministrativa)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.DataVencimento)
            .IsRequired();

        builder.Ignore(p => p.DiasAtraso);
        builder.Ignore(p => p.ValorMulta);
        builder.Ignore(p => p.ValorJuros);
        builder.Ignore(p => p.ValorTotalAtualizado);
    }
}
