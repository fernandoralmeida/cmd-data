using System.Data;
using System.Diagnostics;
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

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".D30"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MSimples>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        c1++;
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');
                        _list.Add(DoFields(fields));
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MSimples>>();

                int parts = Cpu.Count;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count()} -> {parts} : {size}");

                foreach (var rows in _lists)
                    _tasks.Add(new Task(async () =>
                    {
                        var i = 0;
                        var _db = Factory.Data(server);
                        foreach (var row in rows)
                        {
                            i++;
                            await DoInsert(_insert, _db, row, database, datasource);
                        }
                        c2 += i;
                    }));

                Parallel.ForEach(_tasks, t => t.Start());

                _innertimer.Stop();
                Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }

            Log.Storage("Analyzing data!");

            var db = Factory.Data(server);
            await db.WriteAsync(SqlCommands.DeleteNotExist("Simples", "Empresas"), database, datasource);
            await db.ReadAsync(SqlCommands.SelectCommand("Simples"), database, datasource);
            c3 = db.CNPJBase!.Count();

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Error: {ex.Message}");
        }
    });

    public static async Task DatabaseToDataBaseAsync(TServer server, string databaseRead, string datasourceRead, string databaseWrite, string datasourceWrite)
        => await Task.Run(async () =>
        {
            var _timer = new Stopwatch();
            _timer.Start();

            int i = 0;
            var _select = SqlCommands.SelectCommand("Simples");
            var _insert = SqlCommands.InsertCommand("Simples", SqlCommands.Fields_Simples, SqlCommands.Values_Simples);

            var _sqlserver = Factory.Data(TServer.SqlServer);

            var _dataVPS = Factory.Data(server);

            foreach (DataRow row in _sqlserver.ReadAsync(_select, databaseRead, datasourceRead).Result.Rows)
                try
                {
                    _dataVPS.ClearParameters();
                    _dataVPS.AddParameters("@CNPJBase", row[0]);
                    _dataVPS.AddParameters("@OpcaoSimples", row[1]);
                    _dataVPS.AddParameters("@DataOpcaoSimples", row[2]);
                    _dataVPS.AddParameters("@DataExclusaoSimples", row[3]);
                    _dataVPS.AddParameters("@OpcaoMEI", row[4]);
                    _dataVPS.AddParameters("@DataOpcaoMEI", row[5]);
                    _dataVPS.AddParameters("@DataExclusaoMEI", row[6]);
                    await _dataVPS.WriteAsync(_insert, databaseWrite, datasourceWrite);
                    i++;
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }

            _timer.Stop();
            Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        });

    private static MSimples DoFields(string[] fields)
    => new MSimples()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        OpcaoSimples = fields[1].ToString().Replace("\"", "").Trim(),
        DataOpcaoSimples = fields[2].ToString().Replace("\"", "").Trim(),
        DataExclusaoSimples = fields[3].ToString().Replace("\"", "").Trim(),
        OpcaoMEI = fields[4].ToString().Replace("\"", "").Trim(),
        DataOpcaoMEI = fields[5].ToString().Replace("\"", "").Trim(),
        DataExclusaoMEI = fields[6].ToString().Replace("\"", "").Trim()
    };

    private static async Task DoInsert(string sqlcommand, IData data, MSimples model, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", model.CNPJBase!);
        data.AddParameters("@OpcaoSimples", model.OpcaoSimples!);
        data.AddParameters("@DataOpcaoSimples", model.DataOpcaoSimples!);
        data.AddParameters("@DataExclusaoSimples", model.DataExclusaoSimples!);
        data.AddParameters("@OpcaoMEI", model.OpcaoMEI!);
        data.AddParameters("@DataOpcaoMEI", model.DataOpcaoMEI!);
        data.AddParameters("@DataExclusaoMEI", model.DataExclusaoMEI!);
        await data.WriteAsync(sqlcommand, database, datasource);    }

}