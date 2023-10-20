USE [MigraData_RFB]

IF NOT EXISTS (SELECT 1 FROM sys.views WHERE name = 'view_empresas_by_municipio')
BEGIN
EXEC ('
CREATE VIEW view_empresas_by_municipio AS
SELECT
	emp.CNPJBase + est.CNPJDV + est.CNPJOrdem as CNPJ,
	emp.RazaoSocial,
	njd.Codigo + ` ` + njd.Descricao as NaturezaJuridica,
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
	est.TipoLogradouro + ` ` + est.Logradouro as Logradouro,
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
LEFT JOIN Simples snl ON est.CNPJBase = snl.CNPJBase')
END;




