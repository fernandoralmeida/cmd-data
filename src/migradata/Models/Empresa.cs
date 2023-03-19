namespace migradata.Models;

public class Empresa
{
    public Empresa()
    {    }
    public Guid Id { get; set; }
    public string? CNPJBase { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NaturezaJuridica { get; set; }
    public string? QualificacaoResponsavel { get; set; }
    public string? CapitalSocial { get; set; }
    public string? PorteEmpresa { get; set; }
    public string? EnteFederativoResponsavel { get; set; }

}

