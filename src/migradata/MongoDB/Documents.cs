using migradata.Models;

namespace migradata.MongoDB;

public static class Documents
{
    public static MEstabelecimento Estabelecimento(string[] fields)
    => new()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        CNPJOrdem = fields[1].ToString().Replace("\"", "").Trim(),
        CNPJDV = fields[2].ToString().Replace("\"", "").Trim(),
        IdentificadorMatrizFilial = fields[3].ToString().Replace("\"", "").Trim(),
        NomeFantasia = fields[4].ToString().Replace("\"", "").Trim(),
        SituacaoCadastral = fields[5].ToString().Replace("\"", "").Trim(),
        DataSituacaoCadastral = fields[6].ToString().Replace("\"", "").Trim(),
        MotivoSituacaoCadastral = fields[7].ToString().Replace("\"", "").Trim(),
        NomeCidadeExterior = fields[8].ToString().Replace("\"", "").Trim(),
        Pais = fields[9].ToString().Replace("\"", "").Trim(),
        DataInicioAtividade = fields[10].ToString().Replace("\"", "").Trim(),
        CnaeFiscalPrincipal = fields[11].ToString().Replace("\"", "").Trim(),
        CnaeFiscalSecundaria = fields[12].ToString().Replace("\"", "").Trim(),
        TipoLogradouro = fields[13].ToString().Replace("\"", "").Trim(),
        Logradouro = fields[14].ToString().Replace("\"", "").Trim(),
        Numero = fields[15].ToString().Replace("\"", "").Trim(),
        Complemento = fields[16].ToString().Replace("\"", "").Trim(),
        Bairro = fields[17].ToString().Replace("\"", "").Trim(),
        CEP = fields[18].ToString().Replace("\"", "").Trim(),
        UF = fields[19].ToString().Replace("\"", "").Trim(),
        Municipio = fields[20].ToString().Replace("\"", "").Trim(),
        DDD1 = fields[21].ToString().Replace("\"", "").Trim(),
        Telefone1 = fields[22].ToString().Replace("\"", "").Trim(),
        DDD2 = fields[23].ToString().Replace("\"", "").Trim(),
        Telefone2 = fields[24].ToString().Replace("\"", "").Trim(),
        DDDFax = fields[25].ToString().Replace("\"", "").Trim(),
        Fax = fields[26].ToString().Replace("\"", "").Trim(),
        CorreioEletronico = fields[27].ToString().Replace("\"", "").Trim(),
        SituacaoEspecial = fields[28].ToString().Replace("\"", "").Trim(),
        DataSitucaoEspecial = fields[29].ToString().Replace("\"", "").Trim()
    };

    public static MEmpresa Empresa(string[] fields)
    => new()
    {
        CNPJBase = fields[0].ToString().Replace("\"", ""),
        RazaoSocial = fields[1].ToString().Replace("\"", ""),
        NaturezaJuridica = fields[2].ToString().Replace("\"", ""),
        QualificacaoResponsavel = fields[3].ToString().Replace("\"", ""),
        CapitalSocial = fields[4].ToString().Replace("\"", ""),
        PorteEmpresa = fields[5].ToString().Replace("\"", ""),
        EnteFederativoResponsavel = fields[6].ToString().Replace("\"", "")
    };
}