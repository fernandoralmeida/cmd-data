using migradata.SqlServer;
using migradata.Helpers;

namespace migradata.Migrate;

public static class MgNormalize
{
    public static async Task StartAsync(TServer server)
    {
        var data = Factory.Data(server);
        Log.Storage("Checking Connections...");
        Thread.Sleep(3000);
        data.CheckDB();
        Thread.Sleep(3000);
        Log.Storage("Normalizing Database...");
        await data.WriteAsync(SqlCommands.DeletCommand("Cnaes"));
        await data.WriteAsync(SqlCommands.DeletCommand("MotivoSituacaoCadastral"));
        await data.WriteAsync(SqlCommands.DeletCommand("Municipios"));
        await data.WriteAsync(SqlCommands.DeletCommand("NaturezaJuridica"));
        await data.WriteAsync(SqlCommands.DeletCommand("Paises"));
        await data.WriteAsync(SqlCommands.DeletCommand("QualificacaoSocios"));
        await data.WriteAsync(SqlCommands.DeletCommand("Estabelecimentos"));
        await data.WriteAsync(SqlCommands.DeletCommand("Empresas"));
        await data.WriteAsync(SqlCommands.DeletCommand("Socios"));
        await data.WriteAsync(SqlCommands.DeletCommand("Simples"));
        Log.Storage("Normalized Database!");
        Thread.Sleep(3000);
    }
}