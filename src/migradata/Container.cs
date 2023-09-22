using migradata.Helpers;

namespace migradata;

public static class Container
{
    public static async Task Execute(TServer server)
    {
        Log.Storage("Normalize Files");
        //await new NormalizeFiles().Start(@"C:\data");
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

    public static async Task ExecuteToVPS(TServer server)
    {
        await Migrate.MgNormalize.StartAsync(server);
        Log.Storage("Start migration To VPS...");

        await Migrate.MgCnaes.ToVpsAsync(server);
        await Migrate.MgMotivos.ToVpsAsync(server);
        await Migrate.MgMunicipios.ToVpsAsync(server);
        await Migrate.MgNatureza.ToVpsAsync(server);
        await Migrate.MgPaises.ToVpsAsync(server);
        await Migrate.MgQualifica.ToVpsAsync(server);
        await Migrate.MgEstabelecimentos.ToVpsAsync(server);
        await Migrate.MgEmpresas.ToVpsAsync(server);
        await Migrate.MgSocios.ToVpsAsync(server);
        await Migrate.MgSimples.ToVpsAsync(server);

        await Log.Write(Log.StorageLog!);
    }
}