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
    public static async Task StartAsync(TServer server)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;

        var _insert = SqlCommands.InsertCommand("Socios",
                        SqlCommands.Fields_Socios,
                        SqlCommands.Values_Socios);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".SOCIOCSV"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MSocio>();
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
                var _lists = new List<IEnumerable<MSocio>>();

                int parts = Cpu.Count;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count()} -> {parts} : {size}");

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

                Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }
            Log.Storage("Analyzing data!");

            var db = Factory.Data(server);
            await db.WriteAsync(SqlCommands.DeleteNotExist("Socios", "Empresas"), DataBase.MigraData_RFB);
            await db.ReadAsync(SqlCommands.SelectCommand("Socios"), DataBase.MigraData_RFB);
            c3 = db.CNPJBase!.Count();

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Error: {ex.Message}");
        }
    });

    public static async Task ToVpsAsync(TServer server)
        => await Task.Run(async () =>
        {
            var _timer = new Stopwatch();
            _timer.Start();

            int i = 0;
            var _select = SqlCommands.SelectCommand("Socios");
            var _insert = SqlCommands.InsertCommand("Socios", SqlCommands.Fields_Socios, SqlCommands.Values_Socios);

            var _sqlserver = Factory.Data(TServer.SqlServer);

            var _dataVPS = Factory.Data(server);

            foreach (DataRow row in _sqlserver.ReadAsync(_select, DataBase.Sim_RFB_db20210001).Result.Rows)
                try
                {
                    _dataVPS.ClearParameters();
                    _dataVPS.AddParameters("@CNPJBase", row[0]);
                    _dataVPS.AddParameters("@IdentificadorSocio", row[1]);
                    _dataVPS.AddParameters("@NomeRazaoSocio", row[2]);
                    _dataVPS.AddParameters("@CnpjCpfSocio", row[3]);
                    _dataVPS.AddParameters("@QualificacaoSocio", row[4]);
                    _dataVPS.AddParameters("@DataEntradaSociedade", row[5]);
                    _dataVPS.AddParameters("@Pais", row[6]);
                    _dataVPS.AddParameters("@RepresentanteLegal", row[7]);
                    _dataVPS.AddParameters("@NomeRepresentante", row[8]);
                    _dataVPS.AddParameters("@QualificacaoRepresentanteLegal", row[9]);
                    _dataVPS.AddParameters("@FaixaEtaria", row[10]);
                    await _dataVPS.WriteAsync(_insert, DataBase.IndicadoresNET);
                    i++;
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }

            _timer.Stop();
            Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        });

    private static MSocio DoFields(string[] fields)
    => new MSocio()
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

    private static async Task DoInsert(string sqlcommand, IData data, MSocio model)
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
        await data.WriteAsync(sqlcommand, DataBase.MigraData_RFB);
    }

}