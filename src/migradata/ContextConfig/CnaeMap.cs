using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;

public class CnaeMap : IEntityTypeConfiguration<Cnae>
{
    public void Configure(EntityTypeBuilder<Cnae> builder)
    {
        builder.HasNoKey();
        builder.Property(c => c.Codigo)
            .HasColumnType("varchar(10)");
        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(max)");
    }
}

