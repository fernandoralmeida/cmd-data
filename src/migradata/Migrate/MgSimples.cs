using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;
using migradata.SqlServer;

namespace migradata.Migrate;

public static class MgSimples
{
    public static async Task FileToDataBase(TServer server, string database, string datasource)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        var _insert = SqlCommands.InsertCommand("Simples",
                        SqlCommands.Fields_Simples,
                        SqlCommands.Values_Simples);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".D30"))
            {
                var _data = Factory.Data(server);
                var _list = new List<MSimples>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    var _rows = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        _list.Add(DoFields(fields));
                        _rows++;
                        if (c1 % 1000 == 0)
                        {
                            Console.Write($"  {_rows}");
                            Console.Write("\r");
                        }

                        c1++;
                    }
                }

                var _tasks = new List<Task>();
                var _list_datatables = new List<DataTable>();

                int parts = Cpu.Count;
                int size = (_list.Count / parts) + 1;

                for (int p = 0; p < parts; p++)
                {
                    _list_datatables.Add(
                        ModelToDataTable(
                            new MSimples(),
                            _list.Skip(p * size).Take(size)
                        ));
                }

                Log.Storage($"Total: {_list.Count} -> Parts: {parts} -> Rows: {size}");

                int _ntask = -1;
                foreach (var dtables in _list_datatables)
                {
                    _tasks.Add(Task.Run(async () =>
                    {
                        _ntask++;
                        var _timer_task = new Stopwatch();
                        _timer_task.Start();
                        var _db = Factory.Data(server);
                        c2 = dtables.Rows.Count;
                        c3 += c2;
                        await _data.WriteAsync(dtables, "Simples", database, datasource);
                        _timer_task.Start();
                        Log.Storage($"Task: {_ntask} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
                    }));
                }

                await Parallel.ForEachAsync(_tasks,
                    async (t, _) =>
                        await t
                    );
            }
            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Error: {ex.Message}");
        }
    });

    private static MSimples DoFields(string[] fields)
    => new()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        OpcaoSimples = fields[1].ToString().Replace("\"", "").Trim(),
        DataOpcaoSimples = fields[2].ToString().Replace("\"", "").Trim(),
        DataExclusaoSimples = fields[3].ToString().Replace("\"", "").Trim(),
        OpcaoMEI = fields[4].ToString().Replace("\"", "").Trim(),
        DataOpcaoMEI = fields[5].ToString().Replace("\"", "").Trim(),
        DataExclusaoMEI = fields[6].ToString().Replace("\"", "").Trim()
    };

    private static DataTable ModelToDataTable(MSimples model, IEnumerable<MSimples> modelList)
    {
        DataTable dataTable = new();

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