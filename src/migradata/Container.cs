using migradata.Helpers;

namespace migradata;

public static class Container
{
    public static async Task Execute(TServer server)
    {
        Log.Storage("Normalize Files");
        await new NormalizeFiles().Start(@"C:\data");
        await Migrate.MgNormalize.StartAsync(server);
        Log.Storage("Start migration...");

        await Task.WhenAll(
            Migrate.MgCnaes.StartAsync(server),
            Migrate.MgMotivos.StartAsync(server),
            Migrate.MgMunicipios.StartAsync(server),
            Migrate.MgNatureza.StartAsync(server),
            Migrate.MgPaises.StartAsync(server),
            Migrate.MgQualifica.StartAsync(server));

        await Migrate.MgEstabelecimentos.StartAsync(server);
        await Migrate.MgEmpresas.StartAsync(server);
        await Migrate.MgSocios.StartAsync(server);
        await Migrate.MgSimples.StartAsync(server);

        await Log.Write(Log.StorageLog!);
    }
}