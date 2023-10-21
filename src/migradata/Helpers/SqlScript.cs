namespace migradata.Helpers;

public static class SqlScript
{
    public static string SqlServer
    => @"
USE [MigraData_RFB]

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Cnaes')
BEGIN
    CREATE TABLE Cnaes (
        Codigo VARCHAR(10) NULL,
        Descricao varchar(MAX) NULL
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Empresas')
BEGIN
CREATE TABLE Empresas (
    CNPJBase VARCHAR(8) NULL,
    RazaoSocial VARCHAR(255) NULL,
    NaturezaJuridica VARCHAR(5) NULL,
    QualificacaoResponsavel VARCHAR(5) NULL,
    CapitalSocial VARCHAR(20) NULL,
    PorteEmpresa VARCHAR(5) NULL,
    EnteFederativoResponsavel VARCHAR(255) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Estabelecimentos')
BEGIN
CREATE TABLE Estabelecimentos (
    CNPJBase VARCHAR(8) NULL,
    CNPJOrdem VARCHAR(4) NULL,
    CNPJDV VARCHAR(2) NULL,
    IdentificadorMatrizFilial VARCHAR(2) NULL,
    NomeFantasia VARCHAR(255) NULL,
    SituacaoCadastral VARCHAR(2) NULL,
    DataSituacaoCadastral VARCHAR(10) NULL,
    MotivoSituacaoCadastral VARCHAR(2) NULL,
    NomeCidadeExterior VARCHAR(255) NULL,
    Pais VARCHAR(5) NULL,
    DataInicioAtividade VARCHAR(10) NULL,
    CnaeFiscalPrincipal VARCHAR(10) NULL,
    CnaeFiscalSecundaria varchar(MAX) NULL,
    TipoLogradouro VARCHAR(255) NULL,
    Logradouro VARCHAR(255) NULL,
    Numero VARCHAR(255) NULL,
    Complemento VARCHAR(255) NULL,
    Bairro VARCHAR(255) NULL,
    CEP VARCHAR(10) NULL,
    UF VARCHAR(2) NULL,
    Municipio VARCHAR(10) NULL,
    DDD1 VARCHAR(4) NULL,
    Telefone1 VARCHAR(255) NULL,
    DDD2 VARCHAR(4) NULL,
    Telefone2 VARCHAR(255) NULL,
    DDDFax VARCHAR(4) NULL,
    Fax VARCHAR(255) NULL,
    CorreioEletronico VARCHAR(255) NULL,
    SituacaoEspecial VARCHAR(50) NULL,
    DataSitucaoEspecial VARCHAR(10) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'MotivoSituacaoCadastral')
BEGIN
CREATE TABLE MotivoSituacaoCadastral (
    Codigo VARCHAR(2) NULL,
    Descricao varchar(MAX) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Municipios')
BEGIN
CREATE TABLE Municipios (
    Codigo VARCHAR(10) NULL,
    Descricao varchar(MAX) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'NaturezaJuridica')
BEGIN
CREATE TABLE NaturezaJuridica (
    Codigo VARCHAR(5) NULL,
    Descricao varchar(MAX) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Paises')
BEGIN
CREATE TABLE Paises (
    Codigo VARCHAR(5) NULL,
    Descricao varchar(MAX) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'QualificacaoSocios')
BEGIN
CREATE TABLE QualificacaoSocios (
    Codigo VARCHAR(4) NULL,
    Descricao varchar(MAX) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Simples')
BEGIN
CREATE TABLE Simples (
    CNPJBase VARCHAR(8) NULL,
    OpcaoSimples VARCHAR(2) NULL,
    DataOpcaoSimples VARCHAR(10) NULL,
    DataExclusaoSimples VARCHAR(10) NULL,
    OpcaoMEI VARCHAR(2) NULL,
    DataOpcaoMEI VARCHAR(10) NULL,
    DataExclusaoMEI VARCHAR(10) NULL
);
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Socios')
BEGIN

CREATE TABLE Socios (
    CNPJBase VARCHAR(8) NULL,
    IdentificadorSocio VARCHAR(2) NULL,
    NomeRazaoSocio VARCHAR(255) NULL,
    CnpjCpfSocio VARCHAR(15) NULL,
    QualificacaoSocio VARCHAR(4) NULL,
    DataEntradaSociedade VARCHAR(10) NULL,
    Pais VARCHAR(5) NULL,
    RepresentanteLegal VARCHAR(50) NULL,
    NomeRepresentante VARCHAR(255) NULL,
    QualificacaoRepresentanteLegal VARCHAR(4) NULL,
    FaixaEtaria VARCHAR(2) NULL
);
END;";

    public static string MariaDB
    => @"
CREATE TABLE Cnaes (
    Codigo VARCHAR(10) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE Empresas (
    CNPJBase VARCHAR(8) NULL,
    RazaoSocial VARCHAR(255) NULL,
    NaturezaJuridica VARCHAR(5) NULL,
    QualificacaoResponsavel VARCHAR(5) NULL,
    CapitalSocial VARCHAR(20) NULL,
    PorteEmpresa VARCHAR(5) NULL,
    EnteFederativoResponsavel VARCHAR(255) NULL
);

CREATE TABLE Estabelecimentos (
    CNPJBase VARCHAR(8) NULL,
    CNPJOrdem VARCHAR(4) NULL,
    CNPJDV VARCHAR(2) NULL,
    IdentificadorMatrizFilial VARCHAR(2) NULL,
    NomeFantasia VARCHAR(255) NULL,
    SituacaoCadastral VARCHAR(2) NULL,
    DataSituacaoCadastral VARCHAR(10) NULL,
    MotivoSituacaoCadastral VARCHAR(2) NULL,
    NomeCidadeExterior VARCHAR(255) NULL,
    Pais VARCHAR(5) NULL,
    DataInicioAtividade VARCHAR(10) NULL,
    CnaeFiscalPrincipal VARCHAR(10) NULL,
    CnaeFiscalSecundaria varchar(8000) NULL,
    TipoLogradouro VARCHAR(255) NULL,
    Logradouro VARCHAR(255) NULL,
    Numero VARCHAR(255) NULL,
    Complemento VARCHAR(255) NULL,
    Bairro VARCHAR(255) NULL,
    CEP VARCHAR(10) NULL,
    UF VARCHAR(2) NULL,
    Municipio VARCHAR(10) NULL,
    DDD1 VARCHAR(4) NULL,
    Telefone1 VARCHAR(255) NULL,
    DDD2 VARCHAR(4) NULL,
    Telefone2 VARCHAR(255) NULL,
    DDDFax VARCHAR(4) NULL,
    Fax VARCHAR(255) NULL,
    CorreioEletronico VARCHAR(255) NULL,
    SituacaoEspecial VARCHAR(50) NULL,
    DataSitucaoEspecial VARCHAR(10) NULL
);

CREATE TABLE MotivoSituacaoCadastral (
    Codigo VARCHAR(2) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE Municipios (
    Codigo VARCHAR(10) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE NaturezaJuridica (
    Codigo VARCHAR(5) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE Paises (
    Codigo VARCHAR(5) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE QualificacaoSocios (
    Codigo VARCHAR(4) NULL,
    Descricao VARCHAR(8000) NULL
);

CREATE TABLE Simples (
    CNPJBase VARCHAR(8) NULL,
    OpcaoSimples VARCHAR(2) NULL,
    DataOpcaoSimples VARCHAR(10) NULL,
    DataExclusaoSimples VARCHAR(10) NULL,
    OpcaoMEI VARCHAR(2) NULL,
    DataOpcaoMEI VARCHAR(10) NULL,
    DataExclusaoMEI VARCHAR(10) NULL
);

CREATE TABLE Socios (
    CNPJBase VARCHAR(8) NULL,
    IdentificadorSocio VARCHAR(2) NULL,
    NomeRazaoSocio VARCHAR(255) NULL,
    CnpjCpfSocio VARCHAR(15) NULL,
    QualificacaoSocio VARCHAR(4) NULL,
    DataEntradaSociedade VARCHAR(10) NULL,
    Pais VARCHAR(5) NULL,
    RepresentanteLegal VARCHAR(50) NULL,
    NomeRepresentante VARCHAR(255) NULL,
    QualificacaoRepresentanteLegal VARCHAR(4) NULL,
    FaixaEtaria VARCHAR(2) NULL
);";

    public static string Create_View_Empresas
    => @"
CREATE VIEW view_empresas_by_municipio AS
SELECT
	emp.CNPJBase + est.CNPJDV + est.CNPJOrdem as CNPJ,
	emp.RazaoSocial,
	njd.Codigo + ' ' + njd.Descricao as NaturezaJuridica,
	emp.CapitalSocial,
	emp.PorteEmpresa,
	est.IdentificadorMatrizFilial,
	est.NomeFantasia,
	est.SituacaoCadastral,
	est.DataSituacaoCadastral,
	est.DataInicioAtividade,
	est.CnaeFiscalPrincipal,
	atv.Descricao as CnaeDescricao,
	est.CEP,
	est.TipoLogradouro + ' ' + est.Logradouro as Logradouro,
	est.Numero,
	est.Bairro,	
	est.UF,
	mps.Descricao as Municipio,
	snl.OpcaoSimples,
	snl.DataOpcaoSimples,
	snl.DataExclusaoSimples,
	snl.OpcaoMEI,
	snl.DataOpcaoMEI,
	snl.DataExclusaoMEI
FROM Estabelecimentos est
INNER JOIN Empresas emp ON est.CNPJBase = emp.CNPJBase
INNER JOIN CNAEs atv ON est.CnaeFiscalPrincipal = atv.Codigo
INNER JOIN NaturezaJuridica njd ON emp.NaturezaJuridica = njd.Codigo
INNER JOIN Municipios mps ON est.Municipio = mps.Codigo
LEFT JOIN Simples snl ON est.CNPJBase = snl.CNPJBase";

    public static string Create_Table_Empresas =>
    @"
    USE [www_indicadores]

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Empresas')
BEGIN
CREATE TABLE [Empresas] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] varchar(14) NULL,
    [RazaoSocial] varchar(255) NULL,
    [NaturezaJuridica] varchar(255) NULL,
    [CapitalSocial] decimal(18,2) NULL,
    [PorteEmpresa] varchar(2) NULL,
    [IdentificadorMatrizFilial] varchar(1) NULL,
    [NomeFantasia] varchar(255) NULL,
    [SituacaoCadastral] varchar(2) NULL,
    [DataSituacaoCadastral] varchar(8) NULL,
    [DataInicioAtividade] varchar(8) NULL,
    [CnaeFiscalPrincipal] varchar(7) NULL,
    [CnaeDescricao] varchar(255) NULL,
    [Logradouro] varchar(255) NULL,
    [Numero] varchar(10) NULL,
    [Bairro] varchar(255) NULL,
    [CEP] varchar(8) NULL,
    [UF] varchar(2) NULL,
    [Municipio] varchar(50) NULL,
    [OpcaoSimples] varchar(1) NULL,
    [DataOpcaoSimples] varchar(8) NULL,
    [DataExclusaoSimples] varchar(8) NULL,
    [OpcaoMEI] varchar(1) NULL,
    [DataOpcaoMEI] varchar(8) NULL,
    [DataExclusaoMEI] varchar(8) NULL,
    CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id])
);
END;";

    public static string DeleteFrom_RFB => @"
USE [Sim-RFB-db20210001]
GO
DELETE FROM [dbo].[CNAEs]
DELETE FROM [dbo].[Empresas]
DELETE FROM [dbo].[Estabelecimentos]
DELETE FROM [dbo].[MotivoSituacaoCadastral]
DELETE FROM [dbo].[Municipios]
DELETE FROM [dbo].[NaturezaJuridica]
DELETE FROM [dbo].[Paises]
DELETE FROM [dbo].[QualificacaoSocios]
DELETE FROM [dbo].[Simples]
DELETE FROM [dbo].[Socios]
GO";

}