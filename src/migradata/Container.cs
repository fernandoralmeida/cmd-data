using migradata.Helpers;

namespace migradata;

public static class Container
{
    public static async Task MigrateFromFileAsync(TServer server, string database, string datasource)
    {
        Log.Storage("Normalize Files");
        await new NormalizeFiles().Start(@"C:\data");
        await Migrate.MgNormalize.StartAsync(server, database, datasource);
        Log.Storage("Start migration...");

        await Task.WhenAll(
            Migrate.MgCnaes.FileToDataBase(server, database, datasource),
            Migrate.MgMotivos.FileToDataBase(server, database, datasource),
            Migrate.MgMunicipios.FileToDataBase(server, database, datasource),
            Migrate.MgNatureza.FileToDataBase(server, database, datasource),
            Migrate.MgPaises.FileToDataBase(server, database, datasource),
            Migrate.MgQualifica.FileToDataBase(server, database, datasource));

        await Migrate.MgEstabelecimentos.FileToDataBase(server, database, datasource);
        await Migrate.MgEmpresas.FileToDataBase(server, database, datasource);
        await Migrate.MgSocios.FileToDataBase(server, database, datasource);
        await Migrate.MgSimples.FileToDataBase(server, database, datasource);

        await Log.Write(Log.StorageLog!);
    }

    public static async Task DatabaseToDatabaseAsync(TServer server, string databaseRead, string datasourceRead, string databaseWrite, string datasourceWrite)
    {
        await Migrate.MgNormalize.StartAsync(server, databaseWrite, datasourceWrite);
        Log.Storage("Start migration To VPS...");

        await Migrate.MgCnaes.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgMotivos.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgMunicipios.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgNatureza.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgPaises.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgQualifica.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgEstabelecimentos.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgEmpresas.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgSocios.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);
        await Migrate.MgSimples.DatabaseToDataBaseAsync(server, databaseRead, datasourceRead, databaseWrite, datasourceWrite);

        await Log.Write(Log.StorageLog!);
    }
}