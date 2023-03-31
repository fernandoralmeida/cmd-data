namespace migradata.Helpers;

public static class CreateTables
{    
    public static async Task StartAsync(TServer server)
        => await Task.Run(async () =>
        {
            if (server == TServer.SqlServer)
                await new SqlServer.Data().WriteAsync(ScritpDB());

            if (server == TServer.MySql)
                await new MySql.Data().WriteAsync(ScritpDB());
        });


    private static string ScritpDB()
    => @"
CREATE TABLE [Cnaes] (
[Codigo] varchar(10) NULL,
[Descricao] varchar(max) NULL
);

CREATE TABLE [Empresas] (
    [CNPJBase] varchar(10) NULL,
    [RazaoSocial] varchar(255) NULL,
    [NaturezaJuridica] varchar(10) NULL,
    [QualificacaoResponsavel] varchar(5) NULL,
    [CapitalSocial] varchar(255) NULL,
    [PorteEmpresa] varchar(5) NULL,
    [EnteFederativoResponsavel] varchar(255) NULL
);

CREATE TABLE [Estabelecimentos] (
    [CNPJBase] varchar(8) NULL,
    [CNPJOrdem] varchar(4) NULL,
    [CNPJDV] varchar(2) NULL,
    [IdentificadorMatrizFilial] varchar(2) NULL,
    [NomeFantasia] varchar(255) NULL,
    [SituacaoCadastral] varchar(2) NULL,
    [DataSituacaoCadastral] varchar(10) NULL,
    [MotivoSituacaoCadastral] varchar(2) NULL,
    [NomeCidadeExterior] varchar(255) NULL,
    [Pais] varchar(5) NULL,
    [DataInicioAtividade] varchar(10) NULL,
    [CnaeFiscalPrincipal] varchar(255) NULL,
    [CnaeFiscalSecundaria] varchar(max) NULL,
    [TipoLogradouro] varchar(255) NULL,
    [Logradouro] varchar(255) NULL,
    [Numero] varchar(255) NULL,
    [Complemento] varchar(255) NULL,
    [Bairro] varchar(255) NULL,
    [CEP] varchar(10) NULL,
    [UF] varchar(2) NULL,
    [Municipio] varchar(10) NULL,
    [DDD1] varchar(4) NULL,
    [Telefone1] varchar(255) NULL,
    [DDD2] varchar(4) NULL,
    [Telefone2] varchar(255) NULL,
    [DDDFax] varchar(4) NULL,
    [Fax] varchar(255) NULL,
    [CorreioEletronico] varchar(255) NULL,
    [SituacaoEspecial] varchar(255) NULL,
    [DataSitucaoEspecial] varchar(10) NULL
);

CREATE TABLE [MotivoSituacaoCadastral] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);

CREATE TABLE [Municipios] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);

CREATE TABLE [NaturezaJuridica] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);

CREATE TABLE [Paises] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);

CREATE TABLE [QualificacaoSocios] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);

CREATE TABLE [Simples] (
    [CNPJBase] varchar(10) NULL,
    [OpcaoSimples] varchar(2) NULL,
    [DataOpcaoSimples] varchar(10) NULL,
    [DataExclusaoSimples] varchar(10) NULL,
    [OpcaoMEI] varchar(2) NULL,
    [DataOpcaoMEI] varchar(10) NULL,
    [DataExclusaoMEI] varchar(10) NULL
);

CREATE TABLE [Socios] (
    [CNPJBase] varchar(10) NULL,
    [IdentificadorSocio] varchar(2) NULL,
    [NomeRazaoSocio] varchar(255) NULL,
    [CnpjCpfSocio] varchar(50) NULL,
    [QualificacaoSocio] varchar(4) NULL,
    [DataEntradaSociedade] varchar(10) NULL,
    [Pais] nvarchar(max) NULL,
    [RepresentanteLegal] varchar(50) NULL,
    [NomeRepresentante] varchar(255) NULL,
    [QualificacaoRepresentanteLegal] varchar(4) NULL,
    [FaixaEtaria] varchar(2) NULL
);";

}