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

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'UF');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [UF] varchar(999) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'TipoLogradouro');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [TipoLogradouro] varchar(999) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Telefone2');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Telefone2] varchar(999) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Telefone1');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Telefone1] varchar(999) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'SituacaoEspecial');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [SituacaoEspecial] varchar(999) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'SituacaoCadastral');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [SituacaoCadastral] varchar(999) NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Pais');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Pais] varchar(999) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Numero');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Numero] varchar(999) NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'NomeFantasia');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [NomeFantasia] varchar(999) NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'NomeCidadeExterior');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [NomeCidadeExterior] varchar(999) NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Municipio');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Municipio] varchar(999) NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'MotivoSituacaoCadastral');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [MotivoSituacaoCadastral] varchar(999) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Logradouro');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Logradouro] varchar(999) NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'IdentificadorMatrizFilial');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [IdentificadorMatrizFilial] varchar(999) NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Fax');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Fax] varchar(999) NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DataSitucaoEspecial');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DataSitucaoEspecial] varchar(999) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DataSituacaoCadastral');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DataSituacaoCadastral] varchar(999) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DataInicioAtividade');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DataInicioAtividade] varchar(999) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DDDFax');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DDDFax] varchar(999) NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DDD2');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DDD2] varchar(999) NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'DDD1');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [DDD1] varchar(999) NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CorreioEletronico');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CorreioEletronico] varchar(999) NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Complemento');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Complemento] varchar(999) NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CnaeFiscalPrincipal');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CnaeFiscalPrincipal] varchar(999) NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CNPJOrdem');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CNPJOrdem] varchar(999) NULL;
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CNPJDV');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CNPJDV] varchar(999) NULL;
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CNPJBase');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CNPJBase] varchar(999) NULL;
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'CEP');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [CEP] varchar(999) NULL;
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Estabelecimentos]') AND [c].[name] = N'Bairro');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Estabelecimentos] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Estabelecimentos] ALTER COLUMN [Bairro] varchar(999) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230319195343_M_Initial_1', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'RepresentanteLegal');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Socios] ALTER COLUMN [RepresentanteLegal] varchar(999) NULL;
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'QualificacaoSocio');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Socios] ALTER COLUMN [QualificacaoSocio] varchar(999) NULL;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'QualificacaoRepresentanteLegal');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Socios] ALTER COLUMN [QualificacaoRepresentanteLegal] varchar(999) NULL;
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'NomeRepresentante');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Socios] ALTER COLUMN [NomeRepresentante] varchar(999) NULL;
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'NomeRazaoSocio');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Socios] ALTER COLUMN [NomeRazaoSocio] varchar(999) NULL;
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'IdentificadorSocio');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [Socios] ALTER COLUMN [IdentificadorSocio] varchar(999) NULL;
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'FaixaEtaria');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Socios] ALTER COLUMN [FaixaEtaria] varchar(999) NULL;
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'DataEntradaSociedade');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Socios] ALTER COLUMN [DataEntradaSociedade] varchar(999) NULL;
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'CnpjCpfSocio');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Socios] ALTER COLUMN [CnpjCpfSocio] varchar(999) NULL;
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Socios]') AND [c].[name] = N'CNPJBase');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Socios] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Socios] ALTER COLUMN [CNPJBase] varchar(999) NULL;
GO

DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'OpcaoSimples');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var39 + '];');
ALTER TABLE [Simples] ALTER COLUMN [OpcaoSimples] varchar(999) NULL;
GO

DECLARE @var40 sysname;
SELECT @var40 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'OpcaoMEI');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var40 + '];');
ALTER TABLE [Simples] ALTER COLUMN [OpcaoMEI] varchar(999) NULL;
GO

DECLARE @var41 sysname;
SELECT @var41 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'DataOpcaoSimples');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var41 + '];');
ALTER TABLE [Simples] ALTER COLUMN [DataOpcaoSimples] varchar(999) NULL;
GO

DECLARE @var42 sysname;
SELECT @var42 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'DataOpcaoMEI');
IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var42 + '];');
ALTER TABLE [Simples] ALTER COLUMN [DataOpcaoMEI] varchar(999) NULL;
GO

DECLARE @var43 sysname;
SELECT @var43 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'DataExclusaoSimples');
IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var43 + '];');
ALTER TABLE [Simples] ALTER COLUMN [DataExclusaoSimples] varchar(999) NULL;
GO

DECLARE @var44 sysname;
SELECT @var44 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'DataExclusaoMEI');
IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var44 + '];');
ALTER TABLE [Simples] ALTER COLUMN [DataExclusaoMEI] varchar(999) NULL;
GO

DECLARE @var45 sysname;
SELECT @var45 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Simples]') AND [c].[name] = N'CNPJBase');
IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [Simples] DROP CONSTRAINT [' + @var45 + '];');
ALTER TABLE [Simples] ALTER COLUMN [CNPJBase] varchar(999) NULL;
GO

DECLARE @var46 sysname;
SELECT @var46 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'RazaoSocial');
IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var46 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [RazaoSocial] varchar(999) NULL;
GO

DECLARE @var47 sysname;
SELECT @var47 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'QualificacaoResponsavel');
IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var47 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [QualificacaoResponsavel] varchar(999) NULL;
GO

DECLARE @var48 sysname;
SELECT @var48 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'PorteEmpresa');
IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var48 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [PorteEmpresa] varchar(999) NULL;
GO

DECLARE @var49 sysname;
SELECT @var49 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'NaturezaJuridica');
IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var49 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [NaturezaJuridica] varchar(999) NULL;
GO

DECLARE @var50 sysname;
SELECT @var50 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'EnteFederativoResponsavel');
IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var50 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [EnteFederativoResponsavel] varchar(999) NULL;
GO

DECLARE @var51 sysname;
SELECT @var51 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'CapitalSocial');
IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var51 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [CapitalSocial] varchar(999) NULL;
GO

DECLARE @var52 sysname;
SELECT @var52 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empresas]') AND [c].[name] = N'CNPJBase');
IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var52 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [CNPJBase] varchar(999) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230320170150_M_Initial_2', N'7.0.4');
GO

COMMIT;
GO

