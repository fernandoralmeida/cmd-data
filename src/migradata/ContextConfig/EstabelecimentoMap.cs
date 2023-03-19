using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using migradata.Models;

namespace migradata.ContextConfig;
public class EstabelecimentoMap : IEntityTypeConfiguration<Estabelecimento>
{
    public void Configure(EntityTypeBuilder<Estabelecimento> builder)
    {
        builder.HasNoKey();
        builder.Property(c => c.CNPJBase)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CNPJOrdem)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CNPJDV)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.IdentificadorMatrizFilial)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.NomeFantasia)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.SituacaoCadastral)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataSituacaoCadastral)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.MotivoSituacaoCadastral)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.NomeCidadeExterior)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Pais)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataInicioAtividade)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CnaeFiscalPrincipal)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CnaeFiscalSecundaria)
            .HasColumnType("varchar(max)");
        builder.Property(c => c.TipoLogradouro)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Logradouro)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Numero)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Complemento)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Bairro)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CEP)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.UF)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Municipio)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DDD1)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Telefone1)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DDD2)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Telefone2)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DDDFax)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.Fax)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.CorreioEletronico)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.SituacaoEspecial)
            .HasColumnType("varchar(999)");
        builder.Property(c => c.DataSitucaoEspecial)
            .HasColumnType("varchar(999)");
    }
}

