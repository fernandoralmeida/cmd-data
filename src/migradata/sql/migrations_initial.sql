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
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_Cnaes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Empresas] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJBase] varchar(10) NULL,
    [RazaoSocial] varchar(255) NULL,
    [NaturezaJuridica] varchar(10) NULL,
    [QualificacaoResponsavel] varchar(5) NULL,
    [CapitalSocial] varchar(255) NULL,
    [PorteEmpresa] varchar(5) NULL,
    [EnteFederativoResponsavel] varchar(255) NULL,
    CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Estabelecimentos] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJBase] varchar(10) NULL,
    [CNPJOrdem] varchar(5) NULL,
    [CNPJDV] varchar(3) NULL,
    [IdentificadorMatrizFilial] varchar(2) NULL,
    [NomeFantasia] varchar(255) NULL,
    [SituacaoCadastral] varchar(3) NULL,
    [DataSituacaoCadastral] varchar(10) NULL,
    [MotivoSituacaoCadastral] varchar(3) NULL,
    [NomeCidadeExterior] varchar(255) NULL,
    [Pais] varchar(5) NULL,
    [DataInicioAtividade] varchar(10) NULL,
    [CnaeFiscalPrincipal] varchar(255) NULL,
    [CnaeFiscalSecundaria] varchar(max) NULL,
    [TipoLogradouro] varchar(50) NULL,
    [Logradouro] varchar(255) NULL,
    [Numero] varchar(10) NULL,
    [Complemento] varchar(50) NULL,
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
    [DataSitucaoEspecial] varchar(10) NULL,
    CONSTRAINT [PK_Estabelecimentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MotivoSituacaoCadastral] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_MotivoSituacaoCadastral] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Municipios] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_Municipios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NaturezaJuridica] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_NaturezaJuridica] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Paises] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_Paises] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [QualificacaoSocios] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(10) NULL,
    [Descricao] varchar(max) NULL,
    CONSTRAINT [PK_QualificacaoSocios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Simples] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJBase] varchar(10) NULL,
    [OpcaoSimples] varchar(2) NULL,
    [DataOpcaoSimples] varchar(10) NULL,
    [DataExclusaoSimples] varchar(10) NULL,
    [OpcaoMEI] varchar(2) NULL,
    [DataOpcaoMEI] varchar(10) NULL,
    [DataExclusaoMEI] varchar(10) NULL,
    CONSTRAINT [PK_Simples] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Socios] (
    [Id] uniqueidentifier NOT NULL,
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
    [FaixaEtaria] varchar(2) NULL,
    CONSTRAINT [PK_Socios] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230319004117_Initial', N'7.0.4');
GO

COMMIT;
GO

