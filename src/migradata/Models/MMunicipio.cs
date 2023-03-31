namespace migradata.Models;

public class MMunicipio
{
    public MMunicipio() {}
    public string? Codigo { get; set; }
    public string? Descricao { get; set; }

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
    public bool MicroRegiaoJahu(MMunicipio obj)
    {
        /*             
        est.[Coluna 20] = '6607' OR *Jahu*
        est.[Coluna 20] = '6501' OR *Igaraçu do Tiete*
        est.[Coluna 20] = '6235' OR *Bocaina*
        est.[Coluna 20] = '6559' OR *Itapui*
        est.[Coluna 20] = '6245' OR *Boraceia*
        est.[Coluna 20] = '6203' OR *Bariri*
        est.[Coluna 20] = '6541' OR *Itaju*
        est.[Coluna 20] = '6205' OR *Barra Bonita*
        est.[Coluna 20] = '7195' OR *Torrinha*
        est.[Coluna 20] = '6259' OR *Brotas*
        est.[Coluna 20] = '6697' OR *Mineiros do Tiete*
        est.[Coluna 20] = '6835' OR *Pederneiras*
        est.[Coluna 20] = '6383'     *Dois Córregos*
         */

        return
            obj.Codigo == "6607" ||
            obj.Codigo == "6501" ||
            obj.Codigo == "6235" ||
            obj.Codigo == "6559" ||
            obj.Codigo == "6245" ||
            obj.Codigo == "6203" ||
            obj.Codigo == "6541" ||
            obj.Codigo == "6205" ||
            obj.Codigo == "7195" ||
            obj.Codigo == "6259" ||
            obj.Codigo == "6697" ||
            obj.Codigo == "6835" ||
            obj.Codigo == "6383" ||
            obj.Codigo == "6219";
    }
}

