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
            .HasColumnType("varchar(10)");
        builder.Property(c => c.RazaoSocial)
            .HasColumnType("varchar(255)");
        builder.Property(c => c.NaturezaJuridica)
            .HasColumnType("varchar(10)");
        builder.Property(c => c.QualificacaoResponsavel)
            .HasColumnType("varchar(5)");
        builder.Property(c => c.CapitalSocial)
            .HasColumnType("varchar(255)");
        builder.Property(c => c.PorteEmpresa)
            .HasColumnType("varchar(5)");
        builder.Property(c => c.EnteFederativoResponsavel)
            .HasColumnType("varchar(255)");
    }
}

