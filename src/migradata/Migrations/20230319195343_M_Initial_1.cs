using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace migradata.Migrations
{
    /// <inheritdoc />
    public partial class M_Initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoLogradouro",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone2",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone1",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SituacaoEspecial",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCidadeExterior",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotivoSituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificadorMatrizFilial",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataSitucaoEspecial",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataSituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataInicioAtividade",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDDFax",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDD2",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDD1",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorreioEletronico",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CnaeFiscalPrincipal",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJOrdem",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJDV",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Estabelecimentos",
                type: "varchar(999)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "Estabelecimentos",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoLogradouro",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone2",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone1",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SituacaoEspecial",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Estabelecimentos",
                type: "varchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCidadeExterior",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "Estabelecimentos",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MotivoSituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificadorMatrizFilial",
                table: "Estabelecimentos",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataSitucaoEspecial",
                table: "Estabelecimentos",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataSituacaoCadastral",
                table: "Estabelecimentos",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DataInicioAtividade",
                table: "Estabelecimentos",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDDFax",
                table: "Estabelecimentos",
                type: "varchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDD2",
                table: "Estabelecimentos",
                type: "varchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DDD1",
                table: "Estabelecimentos",
                type: "varchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorreioEletronico",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CnaeFiscalPrincipal",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJOrdem",
                table: "Estabelecimentos",
                type: "varchar(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJDV",
                table: "Estabelecimentos",
                type: "varchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJBase",
                table: "Estabelecimentos",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "Estabelecimentos",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Estabelecimentos",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldNullable: true);
        }
    }
}
