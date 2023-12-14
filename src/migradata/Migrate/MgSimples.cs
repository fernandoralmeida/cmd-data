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

        var _insert = SqlCommands.InsertCommand("Simples",
                        SqlCommands.Fields_Simples,
                        SqlCommands.Values_Simples);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".D3"))
            {
                var _list = new List<MSimples>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    var _rows = 0;
                    while (!reader.EndOfStream)
                    {
                        c1++;
                        _rows++;
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');
                        _list.Add(DoFields(fields));
                        if (c1 % 100000 == 0)
                        {
                            Console.Write($"  {_rows}");
                            Console.Write("\r");
                        }
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MSimples>>();

                int parts = Cpu.Count;
                int size = (_list.Count / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count} -> {parts} : {size}");
                Console.Write("\n|");

                foreach (var rows in _lists)
                    _tasks.Add(Task.Run(async () =>
                    {
                        int registrosInseridos = 0;
                        int totalRegistros = size;
                        int progresso = 0;
                        var _db = Factory.Data(server);
                        foreach (var row in rows)
                        {
                            registrosInseridos++;
                            progresso = registrosInseridos * 100 / totalRegistros;
                            c2++;
                            await DoInsert(_insert, _db, row, database, datasource);
                            if (progresso % 10 == 0)
                            {
                                Console.Write($"| {progresso}% ");
                                Console.Write("\r");
                            }
                        }
                    }));


                await Parallel.ForEachAsync(_tasks,
                    async (t, _) =>
                        await t
                    );

                //Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }

            //Log.Storage("Analyzing data!");

            //var db = Factory.Data(server);
            //await db.WriteAsync(SqlCommands.DeleteNotExist("Simples", "Empresas"), database, datasource);
            //await db.ReadAsync(SqlCommands.SelectCommand("Simples"), database, datasource);
            //c3 = db.CNPJBase!.Count();

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
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
        await data.WriteAsync(sqlcommand, database, datasource);
    }

}