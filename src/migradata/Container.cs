using migradata.Helpers;
using migradata.MySql;

namespace migradata;

public static class Container
{
    public static async Task MigrateAsync(TServer server, string database, string datasource)
    {
        Log.Storage("Normalize Files");
        //await FilesCsv.NormalizeAsync(Variables.CommandLine!);

        Log.Storage($"Normalize DataBase {database}!");
        await DataBase.NormalizeAsync(server, database, datasource);        

        Log.Storage("Start migration...");
        //await Migrate.MgCnaes.FileToDataBase(server, database, datasource);
        //await Migrate.MgMotivos.FileToDataBase(server, database, datasource);
        //await Migrate.MgMunicipios.FileToDataBase(server, database, datasource);
        //await Migrate.MgNatureza.FileToDataBase(server, database, datasource);
        //await Migrate.MgPaises.FileToDataBase(server, database, datasource);
        //await Migrate.MgQualifica.FileToDataBase(server, database, datasource);

        //await Migrate.MgEstabelecimentos.FileToDataBase(server, database, datasource);
        //await Migrate.MgEmpresas.FileToDataBase(server, database, datasource);
        //await Migrate.MgSocios.FileToDataBase(server, database, datasource);
        //await Migrate.MgSimples.FileToDataBase(server, database, datasource);

        await DataBase.Normalize_IndicadoresNET(server, DataBase.IndicadoresNET, datasource);

        await Migrate.MgIndicadores
                        .ToDataBase(
                            server: server,
                            databaseOut: DataBase.Sim_RFB_db20210001,
                            databaseIn: DataBase.IndicadoresNET,
                            datasource: DataSource.SqlServer);

        await Log.Write(Log.StorageLog!);
    }

}