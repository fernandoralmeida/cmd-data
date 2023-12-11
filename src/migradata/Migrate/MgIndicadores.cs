using System.Data;
using System.Diagnostics;
using System.Reflection;
using DnsClient.Protocol;
using migradata.Helpers;
using migradata.Models;

namespace migradata.Migrate;

public static class MgIndicadores
{

    public static async Task ToDataBase(TServer server, string databaseOut, string databaseIn, string datasource)
    {
        var c2 = 0;
        var c3 = 0;
        var _timer = new Stopwatch();

        _timer.Start();

        var _list = await DoList(
                            server,
                            SqlCommands.ViewCommand("view_empresas_by_municipio"),
                            databaseOut,
                            datasource);

        var blocos = _list.Chunk(500000).ToList();

        Log.Storage($"Total: {_list.Count} -> Parts: {blocos.Count} -> Rows: {500000}");

        var _tasks = new List<Task>();
        var _t_cont = -1;
        foreach (var bloco in blocos)
        {
            _tasks.Add(Task.Run(async () =>
            {
                Interlocked.Increment(ref _t_cont);
                var _timer_task = new Stopwatch();
                var _data = Factory.Data(server);
                c2 = bloco.Length;
                c3 += c2;
                await _data.
                        WriteAsync(
                            ModelToDataTable(bloco),
                            "Empresas",
                            databaseIn,
                            datasource
                        );
                _timer_task.Stop();
                Log.Storage($"T:{_t_cont} Read: {_list.Count} | Migrated: {c2} | Time: {_timer_task.Elapsed:hh\\:mm\\:ss}");
            }));
        }

        await Task.WhenAll(_tasks);

        _timer.Stop();

        Log.Storage($"Read: {_list.Count} | Migrated: {c3} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
    }

    private static async Task<List<MIndicadoresnet>> DoList(TServer server, string sqlquery, string databaseOut, string datasource)
    {
        var _rows = 0;
        var _db = Factory.Data(server);
        var _list = new List<MIndicadoresnet>();
        Log.Storage($"Starting Migrate to Indicadores");
        Console.Write("\n");
        await foreach (var row in _db.ReadViewAsync(sqlquery, databaseOut, datasource))
        {
            _rows++;
            _list.Add(row);
            Console.Write($"  {_rows}");
            Console.Write("\r");
        }
        return _list;
    }

    private static DataTable ModelToDataTable(IEnumerable<MIndicadoresnet> modelList)
    {
        DataTable dataTable = new();
        var model = new MIndicadoresnet();

        foreach (PropertyInfo property in model.GetType().GetProperties())
            dataTable.Columns.Add(property.Name, property.PropertyType);

        foreach (var item in modelList)
        {
            DataRow row = dataTable.NewRow();

            foreach (PropertyInfo property in model.GetType().GetProperties())
                row[property.Name] = property.GetValue(item);

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

}