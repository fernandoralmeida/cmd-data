using System.Data;
using System.Diagnostics;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;

namespace migradata.Migrate;

public static class MgIndicadores
{
    public static async Task ToDataBase(TServer server, string databaseOut, string databaseIn, string datasource)
    => await Task.Run(async () =>
    {
        var c1 = 0;
        var c2 = 0;
        var c3 = 0;
        var _timer = new Stopwatch();
        var _db = Factory.Data(server);
        var _list = new List<MIndicadoresnet>();
        var _dataRead = await _db.ReadAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource);
        var _insert = SqlCommands.InsertCommand("Empresas",
                SqlCommands.Fields_Indicadores_Empresas,
                SqlCommands.Values_Indicadores_Empresas);

        _timer.Start();

        foreach (DataRow row in _dataRead.Rows)
        {
            _list.Add(new MIndicadoresnet()
            {
                CNPJ = row["CNPJ"].ToString(),
                RazaoSocial = row["RazaoSocial"].ToString(),
                NaturezaJuridica = row["NaturezaJuridica"].ToString(),
                CapitalSocial = Convert.ToDecimal(row["CapitalSocial"]),
                PorteEmpresa = row["PorteEmpresa"].ToString(),
                IdentificadorMatrizFilial = row["IdentificadorMatrizFilial"].ToString(),
                NomeFantasia = row["NomeFantasia"].ToString(),
                SituacaoCadastral = row["SituacaoCadastral"].ToString(),
                DataSituacaoCadastral = row["DataSituacaoCadastral"].ToString(),
                DataInicioAtividade = row["DataInicioAtividade"].ToString(),
                CnaeFiscalPrincipal = row["CnaeFiscalPrincipal"].ToString(),
                CnaeDescricao = row["CnaeDescricao"].ToString(),
                CEP = row["CEP"].ToString(),
                Logradouro = row["Logradouro"].ToString(),
                Numero = row["Numero"].ToString(),
                Bairro = row["Bairro"].ToString(),
                UF = row["UF"].ToString(),
                Municipio = row["Municipio"].ToString(),
                OpcaoSimples = row["OpcaoSimples"].ToString(),
                DataOpcaoSimples = row["DataOpcaoSimples"].ToString(),
                DataExclusaoSimples = row["DataExclusaoSimples"].ToString(),
                OpcaoMEI = row["OpcaoMEI"].ToString(),
                DataOpcaoMEI = row["DataOpcaoMEI"].ToString(),
                DataExclusaoMEI = row["DataExclusaoMEI"].ToString()
            });
        }
        _timer.Stop();

        Log.Storage($"View: {_list.Count()}: {_timer.Elapsed:hh\\:mm\\:ss}");
        Thread.Sleep(3000);

        Log.Storage($"Starting Migrate to Indicadores");

        var _tasks = new List<Task>();
        var _lists = new List<IEnumerable<MIndicadoresnet>>();

        int parts = Cpu.Count;
        int size = (_list.Count / parts) + 1;

        for (int p = 0; p < parts; p++)
            _lists.Add(_list.Skip(p * size).Take(size));

        Log.Storage($"Total: {_list.Count} -> Parts: {parts} -> Rows: {size}");

        int registrosInseridos = 0;
        int totalRegistros = _list.Count;
        int progresso = 0;

        foreach (var rows in _lists)
            _tasks.Add(Task.Run(async () =>
            {
                var i = 0;
                var _db = Factory.Data(server);
                foreach (var row in rows)
                {
                    registrosInseridos++;
                    progresso = registrosInseridos * 100 / totalRegistros;
                    i++;
                    await DoInsert(_insert, _db, row, databaseIn, datasource);
                    if (registrosInseridos % 1000 == 0)
                        Console.Write("#");

                }
                c2 += i;
                Log.Storage($"Part: {i} -> Rows: {c2}");
            }));

        await Parallel.ForEachAsync(_tasks,
            async (t, _) =>
                await t
            );

        _timer.Stop();

        c3 = c2 * parts;

        Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");

    });

    private static async Task DoInsert(string sqlcommand, IData data, MIndicadoresnet est, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@Id", Guid.NewGuid());
        data.AddParameters("@CNPJ", est.CNPJ!);
        data.AddParameters("@RazaoSocial", est.RazaoSocial!);
        data.AddParameters("@NaturezaJuridica", est.NaturezaJuridica!);
        data.AddParameters("@CapitalSocial", est.CapitalSocial!);
        data.AddParameters("@PorteEmpresa", est.PorteEmpresa!);
        data.AddParameters("@IdentificadorMatrizFilial", est.IdentificadorMatrizFilial!);
        data.AddParameters("@NomeFantasia", est.NomeFantasia!);
        data.AddParameters("@SituacaoCadastral", est.SituacaoCadastral!);
        data.AddParameters("@DataSituacaoCadastral", est.DataSituacaoCadastral!);
        data.AddParameters("@DataInicioAtividade", est.DataInicioAtividade!);
        data.AddParameters("@CnaeFiscalPrincipal", est.CnaeFiscalPrincipal!);
        data.AddParameters("@CnaeDescricao", est.CnaeDescricao!);
        data.AddParameters("@CEP", est.CEP!);
        data.AddParameters("@Logradouro", est.Logradouro!);
        data.AddParameters("@Numero", est.Numero!);
        data.AddParameters("@Bairro", est.Bairro!);
        data.AddParameters("@UF", est.UF!);
        data.AddParameters("@Municipio", est.Municipio!);
        data.AddParameters("@OpcaoSimples", est.OpcaoSimples!);
        data.AddParameters("@DataOpcaoSimples", est.DataOpcaoSimples!);
        data.AddParameters("@DataExclusaoSimples", est.DataExclusaoSimples!);
        data.AddParameters("@OpcaoMEI", est.OpcaoMEI!);
        data.AddParameters("@DataOpcaoMEI", est.DataOpcaoMEI!);
        data.AddParameters("@DataExclusaoMEI", est.DataExclusaoMEI!);
        await data.WriteAsync(sqlcommand, database, datasource);
    }
}