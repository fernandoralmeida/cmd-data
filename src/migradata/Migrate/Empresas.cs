using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;
using migradata.Repositories;

namespace migradata.Migrate;

public static class Empresas
{
    public static async Task StartAsync()
=> await Task.Run(async () =>
{
    int i = 0;
    int f = 0;

    var _insert = SqlCommands.InsertCommand("Empresas",
                    SqlCommands.Fields_Empresas,
                    SqlCommands.Values_Empresas);

    var _timer = new Stopwatch();

    try
    {
        var _data = new Generic();

        await _data.ReadAsync(SqlCommands.SelectCommand("Estabelecimentos"));

        foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".EMPRECSV"))
        {

            var _list = new List<Empresa>();
            Console.WriteLine($"Migration File {Path.GetFileName(file)}");
            using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line!.Split(';');
                    _list.Add(DoFields(fields));
                    i++;
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

            var _emp = new List<Empresa>();

            var T1 = Task.Run(() =>
            {
                foreach (var item in _list1)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);

            });

            var T2 = Task.Run(() =>
            {
                foreach (var item in _list2)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T3 = Task.Run(() =>
            {
                foreach (var item in _list3)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T4 = Task.Run(() =>
            {
                foreach (var item in _list4)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T5 = Task.Run(() =>
            {
                foreach (var item in _list5)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T6 = Task.Run(() =>
            {
                foreach (var item in _list6)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T7 = Task.Run(() =>
            {
                foreach (var item in _list7)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            var T8 = Task.Run(() =>
            {
                foreach (var item in _list8)
                    foreach (var s in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                        _emp.Add(item);
            });

            await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);

            var _reps = new Generic();
            await DoList(_insert, _reps, _emp);
            f += _emp.Count();
            Console.WriteLine($"Read: {i}, migrated: {_emp.Count()}, {_timer.Elapsed.TotalMinutes} minutes");
        }
        _timer.Stop();
        Console.WriteLine($"Read: {i}, migrated: {f}, {_timer.Elapsed.TotalMinutes} minutes");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
});

    private static Empresa DoFields(string[] fields)
    => new Empresa()
    {
        CNPJBase = fields[0].ToString().Replace("\"", ""),
        RazaoSocial = fields[1].ToString().Replace("\"", ""),
        NaturezaJuridica = fields[2].ToString().Replace("\"", ""),
        QualificacaoResponsavel = fields[3].ToString().Replace("\"", ""),
        CapitalSocial = fields[4].ToString().Replace("\"", ""),
        PorteEmpresa = fields[5].ToString().Replace("\"", ""),
        EnteFederativoResponsavel = fields[6].ToString().Replace("\"", "")
    };
    private static async Task DoList(string sqlcommand, Generic data, IEnumerable<Empresa> list)
    {
        int i = 0;
        foreach (var item in list)
        {
            i++;
            data.ClearParameters();
            data.AddParameters("@CNPJBase", item.CNPJBase!);
            data.AddParameters("@RazaoSocial", item.RazaoSocial!);
            data.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
            data.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
            data.AddParameters("@CapitalSocial", item.CapitalSocial!);
            data.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
            data.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
            await Task.Run(() => { Console.WriteLine(i); });// data.WriteAsync(sqlcommand);
        }
    }

}