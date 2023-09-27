using migradata.SqlServer;
using migradata.Helpers;

namespace migradata.Migrate;

public static class MgNormalize
{
    public static async Task StartAsync(TServer server, string dbname, string dtsource)
    {
        var data = Factory.Data(server);
        Log.Storage("Checking Connections...");
        Thread.Sleep(3000);
        data.CheckDB(dbname, dtsource);
        Thread.Sleep(3000);
        Log.Storage("Normalizing Database...");
        await data.WriteAsync(SqlCommands.DeletCommand("Cnaes"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("MotivoSituacaoCadastral"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Municipios"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("NaturezaJuridica"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Paises"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("QualificacaoSocios"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Estabelecimentos"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Empresas"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Socios"), dbname, dtsource);
        await data.WriteAsync(SqlCommands.DeletCommand("Simples"), dbname, dtsource);
        Log.Storage("Normalized Database!");
        Thread.Sleep(3000);
    }
}