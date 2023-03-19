IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cnaes] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [Empresas] (
    [CNPJBase] varchar(10) NULL,
    [RazaoSocial] varchar(255) NULL,
    [NaturezaJuridica] varchar(10) NULL,
    [QualificacaoResponsavel] varchar(5) NULL,
    [CapitalSocial] varchar(255) NULL,
    [PorteEmpresa] varchar(5) NULL,
    [EnteFederativoResponsavel] varchar(255) NULL
);
GO

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
    [DDD1] varchar(3) NULL,
    [Telefone1] varchar(255) NULL,
    [DDD2] varchar(3) NULL,
    [Telefone2] varchar(255) NULL,
    [DDDFax] varchar(3) NULL,
    [Fax] varchar(255) NULL,
    [CorreioEletronico] varchar(255) NULL,
    [SituacaoEspecial] varchar(255) NULL,
    [DataSitucaoEspecial] varchar(10) NULL
);
GO

CREATE TABLE [MotivoSituacaoCadastral] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [Municipios] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [NaturezaJuridica] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [Paises] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [QualificacaoSocios] (
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL
);
GO

CREATE TABLE [Simples] (
    [CNPJBase] varchar(10) NULL,
    [OpcaoSimples] varchar(2) NULL,
    [DataOpcaoSimples] varchar(10) NULL,
    [DataExclusaoSimples] varchar(10) NULL,
    [OpcaoMEI] varchar(2) NULL,
    [DataOpcaoMEI] varchar(10) NULL,
    [DataExclusaoMEI] varchar(10) NULL
);
GO

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
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230319192857_M_Initial', N'7.0.4');
GO

COMMIT;
GO

