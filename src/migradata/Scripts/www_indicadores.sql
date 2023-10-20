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
END;
