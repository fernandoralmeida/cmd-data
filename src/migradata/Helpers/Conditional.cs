namespace migradata.Helpers;

public static class Conditional
{
    /// <summary>
    /// Lista contendo os códigos dos múnicipio utilizado pela Receita Federal do Brasil no CNPJ
    /// </summary>
    /// <returns>retorna somente os códigos dos municipio da micro região de Jau e a cidade de Bauru</returns>
    public static List<string> MicroRegionJau()
    {
        return new string[]{
            "6203",
            "6205",
            "6219",
            "6235",
            "6245",
            "6259",
            "6383",
            "6501",
            "6541",
            "6559",
            "6607",
            "6697",
            "6835",
            "7195"
        }.ToList();
    }
}