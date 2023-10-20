namespace migradata.Models;

public class MIndicadoresnet
{

    public MIndicadoresnet()
    { }
    public Guid Id { get; set; }
    public string? CNPJ { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NaturezaJuridica { get; set; }
    public decimal? CapitalSocial { get; set; }
    public string? PorteEmpresa { get; set; }
    public string? IdentificadorMatrizFilial { get; set; }
    public string? NomeFantasia { get; set; }
    public string? SituacaoCadastral { get; set; }
    public string? DataSituacaoCadastral { get; set; }
    public string? DataInicioAtividade { get; set; }
    public string? CnaeFiscalPrincipal { get; set; }
    public string? CnaeDescricao { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? CEP { get; set; }
    public string? UF { get; set; }
    public string? Municipio { get; set; }
    public string? OpcaoSimples { get; set; }
    public string? DataOpcaoSimples { get; set; }
    public string? DataExclusaoSimples { get; set; }
    public string? OpcaoMEI { get; set; }
    public string? DataOpcaoMEI { get; set; }
    public string? DataExclusaoMEI { get; set; }
}