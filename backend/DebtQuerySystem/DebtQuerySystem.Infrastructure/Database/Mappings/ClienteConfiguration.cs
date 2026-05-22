using DebtQuerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebtQuerySystem.Infrastructure.Database.Mappings;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Cpf)
            .IsUnique()
            .HasDatabaseName("IX_Clientes_Cpf");

        builder.HasMany(c => c.Produtos)
            .WithOne()
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.Produtos)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}