using migradata.SqlServer;
using migradata.Helpers;

namespace migradata.Migrate;

public static class MgNormalize
{
    public static async Task StartAsync(TServer server, string dbname)
    {
        var data = Factory.Data(server);
        Log.Storage("Checking Connections...");
        Thread.Sleep(3000);
        data.CheckDB(dbname);
        Thread.Sleep(3000);
        Log.Storage("Normalizing Database...");
        await data.WriteAsync(SqlCommands.DeletCommand("Cnaes"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("MotivoSituacaoCadastral"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Municipios"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("NaturezaJuridica"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Paises"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("QualificacaoSocios"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Estabelecimentos"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Empresas"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Socios"), dbname);
        await data.WriteAsync(SqlCommands.DeletCommand("Simples"), dbname);
        Log.Storage("Normalized Database!");
        Thread.Sleep(3000);
    }
}