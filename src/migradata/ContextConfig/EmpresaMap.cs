using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;
public class EmpresaMap : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasNoKey();
        builder.Property(c => c.CNPJBase)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.RazaoSocial)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.NaturezaJuridica)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.QualificacaoResponsavel)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CapitalSocial)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.PorteEmpresa)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.EnteFederativoResponsavel)
            .HasColumnType("varchar(999)");
    }
}

