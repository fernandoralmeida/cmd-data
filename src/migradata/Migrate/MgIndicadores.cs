using System.Data;
using System.Diagnostics;
using migradata.Helpers;
using migradata.Models;

namespace migradata.Migrate;

public static class MgIndicadores
{
    public static async Task ToDataBase(TServer server, string databaseOut, string databaseIn, string datasource)
    => await Task.Run(async () =>
    {

        var _timer = new Stopwatch();
        var _db = Factory.Data(server);
        var _list = new List<MIndicadoresnet>();        
        var _dataRead = await _db.ReadAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource);

        _timer.Start();

        foreach (DataRow row in _dataRead.Rows)
        {
            _list.Add(new MIndicadoresnet()
            {
                CNPJ = row["CNPJ"] == null ? row["CNPJ"].ToString() : "",
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
        _timer.Reset();
        _timer.Start();

        Log.Storage($"Database {databaseIn} ok");        

        _timer.Stop();

        Log.Storage($"{_timer.Elapsed:hh\\:mm\\:ss}"); 


        
    });
}