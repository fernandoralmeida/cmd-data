using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;
using migradata.SqlServer;

namespace migradata.Migrate;

public static class MgEmpresas
{
    public static async Task StartAsync(TServer server)
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

        var _insert = SqlCommands.InsertCommand("Empresas",
                        SqlCommands.Fields_Empresas,
                        SqlCommands.Values_Empresas);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".EMPRECSV"))
            {

                var _list = new List<MEmpresa>();
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
                    var _db = IoC.Data(server);
                    foreach (var item in _list1)
                        await DoInsert(_insert, _db, item, c1++);

                });

                var T2 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list2)
                        await DoInsert(_insert, _db, item, c2++);
                });

                var T3 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list3)
                        await DoInsert(_insert, _db, item, c3++);
                });

                var T4 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list4)
                        await DoInsert(_insert, _db, item, c4++);
                });

                var T5 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list5)
                        await DoInsert(_insert, _db, item, c5++);
                });

                var T6 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list6)
                        await DoInsert(_insert, _db, item, c6++);
                });

                var T7 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list7)
                        await DoInsert(_insert, _db, item, c7++);
                });

                var T8 = Task.Run(async () =>
                {
                    var _db = IoC.Data(server);
                    foreach (var item in _list8)
                        await DoInsert(_insert, _db, item, c8++);
                });

                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                Console.WriteLine($"Read: {i}, migrated: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
            }

            Console.WriteLine("Analyzing data!");
            
            var db = IoC.Data(server);
            await db.WriteAsync(SqlCommands.DeleteNotExist("Empresas", "Estabelecimentos"));
            await db.ReadAsync(SqlCommands.SelectCommand("Empresas"));
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

    private static MEmpresa DoFields(string[] fields)
    => new MEmpresa()
    {
        CNPJBase = fields[0].ToString().Replace("\"", ""),
        RazaoSocial = fields[1].ToString().Replace("\"", ""),
        NaturezaJuridica = fields[2].ToString().Replace("\"", ""),
        QualificacaoResponsavel = fields[3].ToString().Replace("\"", ""),
        CapitalSocial = fields[4].ToString().Replace("\"", ""),
        PorteEmpresa = fields[5].ToString().Replace("\"", ""),
        EnteFederativoResponsavel = fields[6].ToString().Replace("\"", "")
    };
    
    private static async Task DoInsert(string sqlcommand, IData data, MEmpresa emp, int cont)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", emp.CNPJBase!);
        data.AddParameters("@RazaoSocial", emp.RazaoSocial!);
        data.AddParameters("@NaturezaJuridica", emp.NaturezaJuridica!);
        data.AddParameters("@QualificacaoResponsavel", emp.QualificacaoResponsavel!);
        data.AddParameters("@CapitalSocial", emp.CapitalSocial!);
        data.AddParameters("@PorteEmpresa", emp.PorteEmpresa!);
        data.AddParameters("@EnteFederativoResponsavel", emp.EnteFederativoResponsavel!);
        await data.WriteAsync(sqlcommand);
    }

}