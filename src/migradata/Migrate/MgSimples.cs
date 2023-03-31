using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;
using migradata.Repositories;

namespace migradata.Migrate;

public static class MgSimples
{
    public static async Task StartAsync()
    => await Task.Run(async () =>
    {
        int i = 0;
        int f = 0;
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        int c4 = 0;
        int c5 = 0;
        int c6 = 0;
        int c7 = 0;
        int c8 = 0;

        var _insert = SqlCommands.InsertCommand("Simples",
                        SqlCommands.Fields_Simples,
                        SqlCommands.Values_Simples);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".D30311"))
            {

                var _list = new List<Simples>();
                Console.WriteLine($"Migration File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        i++;
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');
                        _list.Add(DoFields(fields));
                    }
                }

                int parts = 8;
                int size = (_list.Count() / parts) + 1;
                int _end = (size * 8) - _list.Count();
                var _list1 = _list.Skip(0 * size).Take(size);
                var _list2 = _list.Skip(1 * size).Take(size);
                var _list3 = _list.Skip(2 * size).Take(size);
                var _list4 = _list.Skip(3 * size).Take(size);
                var _list5 = _list.Skip(4 * size).Take(size);
                var _list6 = _list.Skip(5 * size).Take(size);
                var _list7 = _list.Skip(6 * size).Take(size);
                var _list8 = _list.Skip(7 * size).Take(size);

                Console.WriteLine($"Reading: {_list.Count()} -> {parts} : {size}");

                var T1 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list1.Take(1))
                        await DoList(_insert, _db, item, c1++);
                });

                var T2 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list2.Take(1))
                        await DoList(_insert, _db, item, c2++);
                });

                var T3 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list3.Take(1))
                        await DoList(_insert, _db, item, c3++);
                });

                var T4 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list4.Take(1))
                        await DoList(_insert, _db, item, c4++);
                });

                var T5 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list5.Take(1))
                        await DoList(_insert, _db, item, c5++);
                });

                var T6 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list6.Take(1))
                        await DoList(_insert, _db, item, c6++);
                });

                var T7 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list7.Take(1))
                        await DoList(_insert, _db, item, c7++);
                });

                var T8 = Task.Run(async () =>
                {
                    var _db = new Generic();
                    foreach (var item in _list8.Take(1))
                        await DoList(_insert, _db, item, c8++);
                });

                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                f += c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8;
                Console.WriteLine($"Read: {i}, migrated: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
            }
            Console.WriteLine("Analyzing data!");
            var db = new Generic();
            await db.WriteAsync(SqlCommands.DeleteNotExist("Simples", "Empresas"));
            f = db.CNPJBase!.Count();
            Console.WriteLine("Reading data!");
            _timer.Stop();
            Console.WriteLine($"Read: {i}, migrated: {f}, {_timer.Elapsed.TotalMinutes} minutes");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    });

    private static Simples DoFields(string[] fields)
    => new Simples()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        OpcaoSimples = fields[1].ToString().Replace("\"", "").Trim(),
        DataOpcaoSimples = fields[2].ToString().Replace("\"", "").Trim(),
        DataExclusaoSimples = fields[3].ToString().Replace("\"", "").Trim(),
        OpcaoMEI = fields[4].ToString().Replace("\"", "").Trim(),
        DataOpcaoMEI = fields[5].ToString().Replace("\"", "").Trim(),
        DataExclusaoMEI = fields[6].ToString().Replace("\"", "").Trim()
    };
    
    private static async Task DoList(string sqlcommand, Generic data, Simples model, int cont)
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