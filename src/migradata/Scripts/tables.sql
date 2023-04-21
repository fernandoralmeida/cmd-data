CREATE TABLE Cnaes (
    Codigo VARCHAR(10) NULL,
    Descricao TEXT NULL
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
    CnaeFiscalSecundaria TEXT NULL,
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
    SituacaoEspecial VARCHAR(10) NULL,
    DataSitucaoEspecial VARCHAR(10) NULL
);

CREATE TABLE MotivoSituacaoCadastral (
    Codigo VARCHAR(2) NULL,
    Descricao TEXT NULL
);

CREATE TABLE Municipios (
    Codigo VARCHAR(10) NULL,
    Descricao TEXT NULL
);

CREATE TABLE NaturezaJuridica (
    Codigo VARCHAR(5) NULL,
    Descricao TEXT NULL
);

CREATE TABLE Paises (
    Codigo VARCHAR(5) NULL,
    Descricao TEXT NULL
);

CREATE TABLE QualificacaoSocios (
    Codigo VARCHAR(4) NULL,
    Descricao TEXT NULL
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
);