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
        data.CheckDB(DataBase.IndicadoresNET);
        Thread.Sleep(3000);
        Log.Storage("Normalizing Database...");
        await data.WriteAsync(SqlCommands.DeletCommand("Cnaes"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("MotivoSituacaoCadastral"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Municipios"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("NaturezaJuridica"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Paises"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("QualificacaoSocios"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Estabelecimentos"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Empresas"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Socios"), DataBase.IndicadoresNET);
        await data.WriteAsync(SqlCommands.DeletCommand("Simples"), DataBase.IndicadoresNET);
        Log.Storage("Normalized Database!");
        Thread.Sleep(3000);
    }
}