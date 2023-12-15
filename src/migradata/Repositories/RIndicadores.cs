using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using migradata.Helpers;
using migradata.Models;

namespace migradata.Repositories;

public static class RIndicadores
{
    public static async Task ToDB(TServer server, string databaseOut, string databaseIn, string datasource)
    {
        var connectionString = $"{datasource}Database={databaseIn};";
        var tableName = "Empresas";

        var _timer = new Stopwatch();
        _timer.Start();

        var _rows = 0;
        var _data = Factory.Data(server);

        Log.Storage($"Starting Migrate to Indicadores");
        Console.Write("\n");

        try
        {
            var _groups = new List<IEnumerable<MIndicadoresnet>>();
            var _indicadores_full = new List<MIndicadoresnet>();
            await foreach (var row in _data.ReadViewAsync(SqlCommands.ViewCommand("view_empresas_by_municipio"), databaseOut, datasource))
            {
                _indicadores_full.Add(
                    new MIndicadoresnet()
                    {
                        Id = Guid.NewGuid(),
                        CNPJ = row.CNPJ!,
                        RazaoSocial = row.RazaoSocial!,
                        NaturezaJuridica = row.NaturezaJuridica!,
                        CapitalSocial = row.CapitalSocial!,
                        PorteEmpresa = row.PorteEmpresa!,
                        IdentificadorMatrizFilial = row.IdentificadorMatrizFilial!,
                        NomeFantasia = row.NomeFantasia!,
                        SituacaoCadastral = row.SituacaoCadastral!,
                        DataSituacaoCadastral = row.DataSituacaoCadastral!,
                        DataInicioAtividade = row.DataInicioAtividade!,
                        CnaeFiscalPrincipal = row.CnaeFiscalPrincipal!,
                        CnaeDescricao = row.CnaeDescricao!,
                        CEP = row.CEP!,
                        Logradouro = row.Logradouro!,
                        Numero = row.Numero!,
                        Bairro = row.Bairro!,
                        UF = row.UF!,
                        Municipio = row.Municipio!,
                        OpcaoSimples = row.OpcaoSimples!,
                        DataOpcaoSimples = row.DataOpcaoSimples!,
                        DataExclusaoSimples = row.DataExclusaoSimples!,
                        OpcaoMEI = row.OpcaoMEI!,
                        DataOpcaoMEI = row.DataOpcaoMEI!,
                        DataExclusaoMEI = row.DataExclusaoMEI!
                    }
                );

                _rows++;
                if (_rows % 1000 == 0)
                {
                    Console.Write($"  {_rows}");
                    Console.Write("\r");
                }
            }

            int _parts = 10;
            int _size_parts = _indicadores_full.Count / _parts;

            //quebra em 10 partes iguais
            _groups = Enumerable
                        .Range(0, _parts)
                        .Select(s => _indicadores_full.Skip(s * _size_parts).Take(_size_parts))
                        .ToList();

            //libera itens para o coletor
            //_indicadores_full.Clear();

            foreach (var parts in _groups)
            {
                var _rows_parts = 0;
                Console.Write("\n|");
                var dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(Guid));
                dataTable.Columns.Add("CNPJ");
                dataTable.Columns.Add("RazaoSocial");
                dataTable.Columns.Add("NaturezaJuridica");
                dataTable.Columns.Add("CapitalSocial");
                dataTable.Columns.Add("PorteEmpresa");
                dataTable.Columns.Add("IdentificadorMatrizFilial");
                dataTable.Columns.Add("NomeFantasia");
                dataTable.Columns.Add("SituacaoCadastral");
                dataTable.Columns.Add("DataSituacaoCadastral");
                dataTable.Columns.Add("DataInicioAtividade");
                dataTable.Columns.Add("CnaeFiscalPrincipal");
                dataTable.Columns.Add("CnaeDescricao");
                dataTable.Columns.Add("CEP");
                dataTable.Columns.Add("Logradouro");
                dataTable.Columns.Add("Numero");
                dataTable.Columns.Add("Bairro");
                dataTable.Columns.Add("UF");
                dataTable.Columns.Add("Municipio");
                dataTable.Columns.Add("OpcaoSimples");
                dataTable.Columns.Add("DataOpcaoSimples");
                dataTable.Columns.Add("DataExclusaoSimples");
                dataTable.Columns.Add("OpcaoMEI");
                dataTable.Columns.Add("DataOpcaoMEI");
                dataTable.Columns.Add("DataExclusaoMEI");

                foreach (var item in parts)
                {
                    _rows_parts++;
                    // Adicionar a linha à DataTable                            
                    DataRow row = dataTable.NewRow();
                    row["Id"] = item.Id;
                    row["CNPJ"] = item.CNPJ!;
                    row["RazaoSocial"] = item.RazaoSocial!;
                    row["NaturezaJuridica"] = item.NaturezaJuridica!;
                    row["CapitalSocial"] = item.CapitalSocial!;
                    row["PorteEmpresa"] = item.PorteEmpresa!;
                    row["IdentificadorMatrizFilial"] = item.IdentificadorMatrizFilial!;
                    row["NomeFantasia"] = item.NomeFantasia!;
                    row["SituacaoCadastral"] = item.SituacaoCadastral!;
                    row["DataSituacaoCadastral"] = item.DataSituacaoCadastral!;
                    row["DataInicioAtividade"] = item.DataInicioAtividade!;
                    row["CnaeFiscalPrincipal"] = item.CnaeFiscalPrincipal!;
                    row["CnaeDescricao"] = item.CnaeDescricao!;
                    row["CEP"] = item.CEP!;
                    row["Logradouro"] = item.Logradouro!;
                    row["Numero"] = item.Numero!;
                    row["Bairro"] = item.Bairro!;
                    row["UF"] = item.UF!;
                    row["Municipio"] = item.Municipio!;
                    row["OpcaoSimples"] = item.OpcaoSimples!;
                    row["DataOpcaoSimples"] = item.DataOpcaoSimples!;
                    row["DataExclusaoSimples"] = item.DataExclusaoSimples!;
                    row["OpcaoMEI"] = item.OpcaoMEI!;
                    row["DataOpcaoMEI"] = item.DataOpcaoMEI!;
                    row["DataExclusaoMEI"] = item.DataExclusaoMEI!;

                    if (_rows_parts % 100 == 0)
                    {
                        Console.Write($"  {_rows_parts}");
                        Console.Write("\r");
                    }
                }
                // Usar SqlBulkCopy para inserir os dados na tabela do banco de dados
                using (var connection = new SqlConnection(connectionString))
                {
                    var _timer_migration = new Stopwatch();
                    _timer_migration.Start();
                    connection.Open(); ;
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        // Mapear as colunas 
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("CNPJ", "CNPJ");
                        bulkCopy.ColumnMappings.Add("RazaoSocial", "RazaoSocial");
                        bulkCopy.ColumnMappings.Add("NaturezaJuridica", "NaturezaJuridica");
                        bulkCopy.ColumnMappings.Add("CapitalSocial", "CapitalSocial");
                        bulkCopy.ColumnMappings.Add("PorteEmpresa", "PorteEmpresa");
                        bulkCopy.ColumnMappings.Add("IdentificadorMatrizFilial", "IdentificadorMatrizFilial");
                        bulkCopy.ColumnMappings.Add("NomeFantasia", "NomeFantasia");
                        bulkCopy.ColumnMappings.Add("SituacaoCadastral", "SituacaoCadastral");
                        bulkCopy.ColumnMappings.Add("DataSituacaoCadastral", "DataSituacaoCadastral");
                        bulkCopy.ColumnMappings.Add("DataInicioAtividade", "DataInicioAtividade");
                        bulkCopy.ColumnMappings.Add("CnaeFiscalPrincipal", "CnaeFiscalPrincipal");
                        bulkCopy.ColumnMappings.Add("CnaeDescricao", "CnaeDescricao");
                        bulkCopy.ColumnMappings.Add("CEP", "CEP");
                        bulkCopy.ColumnMappings.Add("Logradouro", "Logradouro");
                        bulkCopy.ColumnMappings.Add("Numero", "Numero");
                        bulkCopy.ColumnMappings.Add("Bairro", "Bairro");
                        bulkCopy.ColumnMappings.Add("UF", "UF");
                        bulkCopy.ColumnMappings.Add("Municipio", "Municipio");
                        bulkCopy.ColumnMappings.Add("OpcaoSimples", "OpcaoSimples");
                        bulkCopy.ColumnMappings.Add("DataOpcaoSimples", "DataOpcaoSimples");
                        bulkCopy.ColumnMappings.Add("DataExclusaoSimples", "DataExclusaoSimples");
                        bulkCopy.ColumnMappings.Add("OpcaoMEI", "OpcaoMEI");
                        bulkCopy.ColumnMappings.Add("DataOpcaoMEI", "DataOpcaoMEI");
                        bulkCopy.ColumnMappings.Add("DataExclusaoMEI", "DataExclusaoMEI");

                        bulkCopy.BulkCopyTimeout = 0;
                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
                    _timer_migration.Stop();
                    Log.Storage($"Lines: {_rows_parts} | Migrated: {_rows_parts} | Time: {_timer_migration.Elapsed:hh\\:mm\\:ss}");
                }
                dataTable.Dispose();
            }

            _timer.Stop();
            Log.Storage($"Lines: {_rows} | Migrated: {_rows} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    }

}