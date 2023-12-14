using migradata.Helpers;

namespace migradata;

public static class Container
{
    public static async Task MigrateAsync(TServer server, string database, string datasource)
    {
        /**/
        Log.Storage("Normalize Files");
        //await FilesCsv.NormalizeAsync(Variables.CommandLine!);

        Log.Storage($"Normalize DataBase {database}!");
        await DataBase.NormalizeAsync(server, database, datasource);

        Log.Storage("Start migration...");
        await Repositories.RCnaes.DoFileToDB(server, database, datasource);
        await Repositories.RMotivos.DoFileToDB(server, database, datasource);
        await Repositories.RMunicipios.DoFileToDB(server, database, datasource);
        await Repositories.RNatureza.DoFileToDB(server, database, datasource);
        await Repositories.RPaises.DoFileToDB(server, database, datasource);
        await Repositories.RQualifica.DoFileToDB(server, database, datasource);

        await Repositories.REstabelecimentos.DoFileToDBBulkCopy(server, database, datasource);
        await Repositories.REmpresas.DoFileToDB(server, database, datasource);
        await Repositories.RSocios.DoFileToDB(server, database, datasource);
        await Repositories.RSimples.DoFileToDB(server, database, datasource);


        await DataBase.Normalize_IndicadoresNET(server, DataBase.IndicadoresNET, datasource);

        await Repositories.RIndicadores
                        .ToDB(
                            server: server,
                            databaseOut: DataBase.MigraData_RFB,
                            databaseIn: DataBase.IndicadoresNET,
                            datasource: DataSource.SqlServer);

        await Log.Write(Log.StorageLog!);
    }

}