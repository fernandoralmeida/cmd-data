using System.Data;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;
using migradata.SqlServer;

namespace migradata.Migrate;

public static class MgSocios
{
    public static async Task FileToDataBase(TServer server, string database, string datasource)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;

        var _insert = SqlCommands.InsertCommand("Socios",
                        SqlCommands.Fields_Socios,
                        SqlCommands.Values_Socios);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".SOCIOCSV"))
            {
                var _list = new List<MSocio>();
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
                        {
                            Console.Write($"  {_rows}");
                            Console.Write("\r");
                        }
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MSocio>>();

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

                //Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed:hh\\:mm\\:ss}");
            }
            //var db = Factory.Data(server);
            //await db.WriteAsync(SqlCommands.DeleteNotExist("Socios", "Empresas"), database, datasource);
            //await db.ReadAsync(SqlCommands.SelectCommand("Socios"), database, datasource);
            //c3 = db.CNPJBase!.Count();

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Error: {ex.Message}");
        }
    });

    private static MSocio DoFields(string[] fields)
    => new()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        IdentificadorSocio = fields[1].ToString().Replace("\"", "").Trim(),
        NomeRazaoSocio = fields[2].ToString().Replace("\"", "").Trim(),
        CnpjCpfSocio = fields[3].ToString().Replace("\"", "").Trim(),
        QualificacaoSocio = fields[4].ToString().Replace("\"", "").Trim(),
        DataEntradaSociedade = fields[5].ToString().Replace("\"", "").Trim(),
        Pais = fields[6].ToString().Replace("\"", "").Trim(),
        RepresentanteLegal = fields[7].ToString().Replace("\"", "").Trim(),
        NomeRepresentante = fields[8].ToString().Replace("\"", "").Trim(),
        QualificacaoRepresentanteLegal = fields[9].ToString().Replace("\"", "").Trim(),
        FaixaEtaria = fields[10].ToString().Replace("\"", "").Trim()
    };

    private static async Task DoInsert(string sqlcommand, IData data, MSocio model, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", model.CNPJBase!);
        data.AddParameters("@IdentificadorSocio", model.IdentificadorSocio!);
        data.AddParameters("@NomeRazaoSocio", model.NomeRazaoSocio!);
        data.AddParameters("@CnpjCpfSocio", model.CnpjCpfSocio!);
        data.AddParameters("@QualificacaoSocio", model.QualificacaoSocio!);
        data.AddParameters("@DataEntradaSociedade", model.DataEntradaSociedade!);
        data.AddParameters("@Pais", model.Pais!);
        data.AddParameters("@RepresentanteLegal", model.RepresentanteLegal!);
        data.AddParameters("@NomeRepresentante", model.NomeRepresentante!);
        data.AddParameters("@QualificacaoRepresentanteLegal", model.QualificacaoRepresentanteLegal!);
        data.AddParameters("@FaixaEtaria", model.FaixaEtaria!);
        await data.WriteAsync(sqlcommand, database, datasource);
    }

}