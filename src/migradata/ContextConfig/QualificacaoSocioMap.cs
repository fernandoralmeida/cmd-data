using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;
public class QualificacaoSocioMap : IEntityTypeConfiguration<QualificacaoSocio>
{
    public void Configure(EntityTypeBuilder<QualificacaoSocio> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Codigo)
            .HasColumnType("varchar(10)");
        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(max)");
    }
}

