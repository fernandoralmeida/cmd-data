namespace migradata.Models;

public class MotivoSituacaoCadastral
{
    public MotivoSituacaoCadastral() {}
    public Guid Id { get; set; }
    public string? Codigo
    {
        get; set;
    }
    public string? Descricao
    {
        get; set;
    }
}

