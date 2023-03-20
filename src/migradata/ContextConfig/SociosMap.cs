using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;
public class SociosMap : IEntityTypeConfiguration<Socio>
{
    public void Configure(EntityTypeBuilder<Socio> builder)
    {
        builder.HasNoKey();
        builder.Property(c => c.CNPJBase)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.IdentificadorSocio)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.NomeRazaoSocio)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CnpjCpfSocio)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.QualificacaoSocio)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataEntradaSociedade)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.RepresentanteLegal)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.NomeRepresentante)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.QualificacaoRepresentanteLegal)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.FaixaEtaria)
            .HasColumnType("varchar(999)");
    }
}

