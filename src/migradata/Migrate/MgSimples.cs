using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;
using migradata.SqlServer;

namespace migradata.Migrate;

public static class MgSimples
{
    public static async Task StartAsync(TServer server)
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

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".D30311"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MSimples>();
                await new Log().Write($"Reading File {Path.GetFileName(file)}");
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

                int parts = 10;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                await new Log().Write($"Migrating: {_list.Count()} -> {parts} : {size}");

                foreach (var rows in _lists)
                    _tasks.Add(Task.Run(async () =>
                    {
                        var i = 0;
                        var _db = Factory.Data(server);
                        foreach (var row in rows)
                        {
                            i++;
                            await DoInsert(_insert, _db, row);
                        }
                        c2 += i;
                    }));

                await Task.WhenAll(_tasks);

                _innertimer.Stop();
                await new Log().Write($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }
           
            await new Log().Write("Analyzing data!");
            
            var db = Factory.Data(server);
            await db.WriteAsync(SqlCommands.DeleteNotExist("Simples", "Empresas"));
            await db.ReadAsync(SqlCommands.SelectCommand("Simples"));
            c3 = db.CNPJBase!.Count();
            
            _timer.Stop();
            await new Log().Write($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        }
        catch (Exception ex)
        {
            await new Log().Write($"Error: {ex.Message}");
        }
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
    
    private static async Task DoInsert(string sqlcommand, IData data, MSimples model)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", model.CNPJBase!);
        data.AddParameters("@OpcaoSimples", model.OpcaoSimples!);
        data.AddParameters("@DataOpcaoSimples", model.DataOpcaoSimples!);
        data.AddParameters("@DataExclusaoSimples", model.DataExclusaoSimples!);
        data.AddParameters("@OpcaoMEI", model.OpcaoMEI!);
        data.AddParameters("@DataOpcaoMEI", model.DataOpcaoMEI!);
        data.AddParameters("@DataExclusaoMEI", model.DataExclusaoMEI!);
        await data.WriteAsync(sqlcommand);        
    }

}