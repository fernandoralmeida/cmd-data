using System.Diagnostics;
using migradata.Models;

namespace migradata.Helpers;

public static class DataBase
{
    public static readonly string MigraData_RFB = @"MigraData_RFB";

    public static readonly string Sim_RFB_db20210001 = @"Sim-RFB-db20210001";

    public static readonly string IndicadoresNET = @"www_indicadores";

    public static async Task NormalizeAsync(TServer server, string dbname, string dtsource)
    {
        var _timer = new Stopwatch();
        _timer.Start();
        var data = Factory.Data(server);
        Log.Storage("Checking Connections...");

        if (await data.DbExists(dbname, dtsource))
        {
            Log.Storage("Normalizing Database...");
            await data.ExecuteAsync(SqlCommands.DeletCommand("Cnaes"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("MotivoSituacaoCadastral"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Municipios"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("NaturezaJuridica"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Paises"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("QualificacaoSocios"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Estabelecimentos"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Empresas"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Socios"), dbname, dtsource);
            await data.ExecuteAsync(SqlCommands.DeletCommand("Simples"), dbname, dtsource);
        }
        else
        {
            await data.CreateDB(
                datasource: dtsource,
                database: dbname,
                new List<MSqlCommand>()
                {
                    new(){Name = "Tables", Command = SqlScript.SqlServer },
                    new(){Name = "Views", Command = SqlScript.Create_View_Empresas }
                });

            //Thread.Sleep(3000);

            Log.Storage("Checking Connections...");

            await data.DbExists(dbname, dtsource);
        }
        _timer.Stop();
        Log.Storage($"Normalized Database! {_timer.Elapsed:hh\\:mm\\:ss}");
       // Thread.Sleep(3000);
    }

    public static async Task Normalize_IndicadoresNET(TServer server, string dbname, string dtsource)
    => await Task.Run(async ()=> {
        var _timer = new Stopwatch();
        _timer.Start();
        var data = Factory.Data(server);
        Log.Storage("Checking Connections...");

        if (await data.DbExists(dbname, dtsource))
        {
            Log.Storage("Normalizing Database...");
            await data.ExecuteAsync(SqlCommands.DeletCommand("Empresas"), dbname, dtsource);
        }
        else
        {
            await data.CreateDB(
                datasource: dtsource,
                database: dbname,
                new List<MSqlCommand>()
                {
                    new(){Name = "Tables", Command = SqlScript.Create_Table_Empresas }
                });

            Thread.Sleep(3000);

            Log.Storage("Checking Connections...");

            await data.DbExists(dbname, dtsource);
        }
        _timer.Stop();
        Log.Storage($"Normalized Database! {_timer.Elapsed:hh\\:mm\\:ss}");
        Thread.Sleep(3000);
    });

}