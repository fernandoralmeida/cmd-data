using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;
public class SimplesMap : IEntityTypeConfiguration<Simples>
{
    public void Configure(EntityTypeBuilder<Simples> builder)
    {
        builder.HasNoKey();
        builder.Property(c => c.CNPJBase)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.OpcaoSimples)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataOpcaoSimples)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataExclusaoSimples)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.OpcaoMEI)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataOpcaoMEI)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataExclusaoMEI)
            .HasColumnType("varchar(999)");
    }
}

