using System.Data;
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

        int c1 = 0;
        int c2 = 0;

        var _insert = SqlCommands.InsertCommand("Empresas",
                        SqlCommands.Fields_Empresas,
                        SqlCommands.Values_Empresas);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".EMPRECSV"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MEmpresa>();
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
                var _lists = new List<IEnumerable<MEmpresa>>();

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
            await db.WriteAsync(SqlCommands.DeleteNotExist("Empresas", "Estabelecimentos"), DataBase.MigraData_RFB);
            var _cont = await db.ReadAsync(SqlCommands.SelectCommand("Empresas"), DataBase.MigraData_RFB);

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {_cont.Rows.Count} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
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
        var _select = SqlCommands.SelectCommand("Empresas");
        var _insert = SqlCommands.InsertCommand("Empresas", SqlCommands.Fields_Empresas, SqlCommands.Values_Empresas);

        var _sqlserver = Factory.Data(TServer.SqlServer);

        var _dataVPS = Factory.Data(server);

        foreach (DataRow row in _sqlserver.ReadAsync(_select, DataBase.Sim_RFB_db20210001).Result.Rows)
            try
            {
                _dataVPS.ClearParameters();
                _dataVPS.AddParameters("@CNPJBase", row[0]);
                _dataVPS.AddParameters("@RazaoSocial", row[1]);
                _dataVPS.AddParameters("@NaturezaJuridica", row[2]);
                _dataVPS.AddParameters("@QualificacaoResponsavel", row[3]);
                _dataVPS.AddParameters("@CapitalSocial", row[4]);
                _dataVPS.AddParameters("@PorteEmpresa", row[5]);
                _dataVPS.AddParameters("@EnteFederativoResponsavel", row[6]);
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

    private static async Task DoInsert(string sqlcommand, IData data, MEmpresa emp)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", emp.CNPJBase!);
        data.AddParameters("@RazaoSocial", emp.RazaoSocial!);
        data.AddParameters("@NaturezaJuridica", emp.NaturezaJuridica!.Length <= 4 ? emp.NaturezaJuridica! : "0000");
        data.AddParameters("@QualificacaoResponsavel", emp.QualificacaoResponsavel!.Length <= 2 ? emp.QualificacaoResponsavel! : "00");
        data.AddParameters("@CapitalSocial", emp.CapitalSocial!);
        data.AddParameters("@PorteEmpresa", emp.PorteEmpresa!.Length <= 2 ? emp.PorteEmpresa! : "00");
        data.AddParameters("@EnteFederativoResponsavel", emp.EnteFederativoResponsavel!);
        //await Task.Run(()=>{});
        await data.WriteAsync(sqlcommand, DataBase.MigraData_RFB);
    }

}