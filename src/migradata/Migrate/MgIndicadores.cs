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
        var _timer = new Stopwatch();
        var _db = Factory.Data(server);
        
        var _insert = SqlCommands.InsertCommand("Empresas",
                SqlCommands.Fields_Indicadores_Empresas,
                SqlCommands.Values_Indicadores_Empresas);

        _timer.Start();
        var _rows = 0;
        Log.Storage($"Starting Migrate to Indicadores");
        Console.Write("\n");
        var _list = new List<MIndicadoresnet>();
        await foreach (var row in _db.ReadViewAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource))
        {
            _rows++;
            _list.Add(row);
            if (c1 % 10000 == 0)
            {
                Console.Write($"  {_rows}");
                Console.Write("\r");
            }
        }
        _timer.Stop();

        Log.Storage($"View: {_list.Count}: {_timer.Elapsed:hh\\:mm\\:ss}");

        var _tasks = new List<Task>();
        var _lists = new List<IEnumerable<MIndicadoresnet>>();

        int parts = Cpu.Count;
        int size = (_list.Count / parts) + 1;

        for (int p = 0; p < parts; p++)
            _lists.Add(_list.Skip(p * size).Take(size));

        Log.Storage($"Total: {_list.Count} -> Parts: {parts} -> Rows: {size}");
        Console.Write("\n|");

        foreach (var rows in _lists)
            _tasks.Add(Task.Run(async () =>
            {
                var _db = Factory.Data(server);
                int registrosInseridos = 0;
                int totalRegistros = size;
                int progresso = 0;
                foreach (var row in rows)
                {
                    registrosInseridos++;
                    progresso = registrosInseridos * 100 / totalRegistros;
                    c2++;
                    await DoInsert(_insert, _db, row, databaseIn, datasource);
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

        _timer.Stop();

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