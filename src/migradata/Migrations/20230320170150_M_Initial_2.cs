using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace migradata.Migrations
{
    /// <inheritdoc />
    public partial class M_Initial_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RepresentanteLegal",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoSocio",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoRepresentanteLegal",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRepresentante",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRazaoSocio",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificadorSocio",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaixaEtaria",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataEntradaSociedade",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CnpjCpfSocio",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Socios",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpcaoSimples",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpcaoMEI",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataOpcaoSimples",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataOpcaoMEI",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataExclusaoSimples",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataExclusaoMEI",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Simples",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoResponsavel",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PorteEmpresa",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NaturezaJuridica",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnteFederativoResponsavel",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapitalSocial",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Empresas",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RepresentanteLegal",
                table: "Socios",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoSocio",
                table: "Socios",
                type: "varchar(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoRepresentanteLegal",
                table: "Socios",
                type: "varchar(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRepresentante",
                table: "Socios",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRazaoSocio",
                table: "Socios",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificadorSocio",
                table: "Socios",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaixaEtaria",
                table: "Socios",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataEntradaSociedade",
                table: "Socios",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CnpjCpfSocio",
                table: "Socios",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Socios",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpcaoSimples",
                table: "Simples",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpcaoMEI",
                table: "Simples",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataOpcaoSimples",
                table: "Simples",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataOpcaoMEI",
                table: "Simples",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataExclusaoSimples",
                table: "Simples",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataExclusaoMEI",
                table: "Simples",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Simples",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualificacaoResponsavel",
                table: "Empresas",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PorteEmpresa",
                table: "Empresas",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NaturezaJuridica",
                table: "Empresas",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnteFederativoResponsavel",
                table: "Empresas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapitalSocial",
                table: "Empresas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Empresas",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);
        }
    }
}
