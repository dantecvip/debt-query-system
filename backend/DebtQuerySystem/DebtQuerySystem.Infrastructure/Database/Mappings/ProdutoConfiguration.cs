using DebtQuerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtQuerySystem.Infrastructure.Database.Mappings;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(p => p.Parcelas)
            .WithOne()
            .HasForeignKey(parcela => parcela.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Parcelas)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
