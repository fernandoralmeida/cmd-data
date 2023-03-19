using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace migradata.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cnaes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cnaes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJBase = table.Column<string>(type: "varchar(10)", nullable: true),
                    RazaoSocial = table.Column<string>(type: "varchar(255)", nullable: true),
                    NaturezaJuridica = table.Column<string>(type: "varchar(10)", nullable: true),
                    QualificacaoResponsavel = table.Column<string>(type: "varchar(5)", nullable: true),
                    CapitalSocial = table.Column<string>(type: "varchar(255)", nullable: true),
                    PorteEmpresa = table.Column<string>(type: "varchar(5)", nullable: true),
                    EnteFederativoResponsavel = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJBase = table.Column<string>(type: "varchar(10)", nullable: true),
                    CNPJOrdem = table.Column<string>(type: "varchar(5)", nullable: true),
                    CNPJDV = table.Column<string>(type: "varchar(3)", nullable: true),
                    IdentificadorMatrizFilial = table.Column<string>(type: "varchar(2)", nullable: true),
                    NomeFantasia = table.Column<string>(type: "varchar(255)", nullable: true),
                    SituacaoCadastral = table.Column<string>(type: "varchar(3)", nullable: true),
                    DataSituacaoCadastral = table.Column<string>(type: "varchar(10)", nullable: true),
                    MotivoSituacaoCadastral = table.Column<string>(type: "varchar(3)", nullable: true),
                    NomeCidadeExterior = table.Column<string>(type: "varchar(255)", nullable: true),
                    Pais = table.Column<string>(type: "varchar(5)", nullable: true),
                    DataInicioAtividade = table.Column<string>(type: "varchar(10)", nullable: true),
                    CnaeFiscalPrincipal = table.Column<string>(type: "varchar(255)", nullable: true),
                    CnaeFiscalSecundaria = table.Column<string>(type: "varchar(max)", nullable: true),
                    TipoLogradouro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(255)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(255)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(10)", nullable: true),
                    DDD1 = table.Column<string>(type: "varchar(3)", nullable: true),
                    Telefone1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    DDD2 = table.Column<string>(type: "varchar(3)", nullable: true),
                    Telefone2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    DDDFax = table.Column<string>(type: "varchar(3)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(255)", nullable: true),
                    CorreioEletronico = table.Column<string>(type: "varchar(255)", nullable: true),
                    SituacaoEspecial = table.Column<string>(type: "varchar(255)", nullable: true),
                    DataSitucaoEspecial = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivoSituacaoCadastral",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoSituacaoCadastral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturezaJuridica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturezaJuridica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualificacaoSocios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificacaoSocios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJBase = table.Column<string>(type: "varchar(10)", nullable: true),
                    OpcaoSimples = table.Column<string>(type: "varchar(2)", nullable: true),
                    DataOpcaoSimples = table.Column<string>(type: "varchar(10)", nullable: true),
                    DataExclusaoSimples = table.Column<string>(type: "varchar(10)", nullable: true),
                    OpcaoMEI = table.Column<string>(type: "varchar(2)", nullable: true),
                    DataOpcaoMEI = table.Column<string>(type: "varchar(10)", nullable: true),
                    DataExclusaoMEI = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJBase = table.Column<string>(type: "varchar(10)", nullable: true),
                    IdentificadorSocio = table.Column<string>(type: "varchar(2)", nullable: true),
                    NomeRazaoSocio = table.Column<string>(type: "varchar(255)", nullable: true),
                    CnpjCpfSocio = table.Column<string>(type: "varchar(50)", nullable: true),
                    QualificacaoSocio = table.Column<string>(type: "varchar(4)", nullable: true),
                    DataEntradaSociedade = table.Column<string>(type: "varchar(10)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepresentanteLegal = table.Column<string>(type: "varchar(50)", nullable: true),
                    NomeRepresentante = table.Column<string>(type: "varchar(255)", nullable: true),
                    QualificacaoRepresentanteLegal = table.Column<string>(type: "varchar(4)", nullable: true),
                    FaixaEtaria = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cnaes");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");

            migrationBuilder.DropTable(
                name: "MotivoSituacaoCadastral");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "NaturezaJuridica");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "QualificacaoSocios");

            migrationBuilder.DropTable(
                name: "Simples");

            migrationBuilder.DropTable(
                name: "Socios");
        }
    }
}
