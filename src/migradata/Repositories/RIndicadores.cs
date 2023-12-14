using System.Diagnostics;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;

namespace migradata.Repositories;

public static class RIndicadores
{
    public static async Task ToDB(TServer server, string databaseOut, string databaseIn, string datasource)
    {
        var _timer = new Stopwatch();

        _timer.Start();

        var _rows = 0;
        var _data = Factory.Data(server);

        Log.Storage($"Starting Migrate to Indicadores");
        Console.Write("\n");

        var _insert = SqlCommands.InsertCommand("Empresas",
                SqlCommands.Fields_Indicadores_Empresas,
                SqlCommands.Values_Indicadores_Empresas);

        await foreach (var row in _data.ReadViewAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource))
        {
            _rows++;
            await DoInsert(_insert, _data, row, databaseIn, datasource);
            if (_rows % 100000 == 0)
            {
                Console.Write($"  {_rows}");
                Console.Write("\r");
            }
        }

        _timer.Stop();

        Log.Storage($"Read: {_rows} | Migrated: {_rows} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
    }
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
        await data.ExecuteAsync(sqlcommand, database, datasource);
    }

}