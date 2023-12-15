using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;

namespace migradata.Repositories;



public static class RSimples
{
    public static async Task DoFileToDBBulkCopy(TServer server, string database, string datasource)
    {
        int _count = 0;
        var connectionString = $"{datasource}Database={database};";
        var tableName = "Simples";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".D3"))
            {
                _count = 0;

                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                var _groups = new List<IEnumerable<MSimples>>();
                var _lista_simples_completa = new List<MSimples>();

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');

                        _count++;

                        _lista_simples_completa.Add(new MSimples()
                        {
                            CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
                            OpcaoSimples = fields[1].ToString().Replace("\"", "").Trim(),
                            DataOpcaoSimples = fields[2].ToString().Replace("\"", "").Trim(),
                            DataExclusaoSimples = fields[3].ToString().Replace("\"", "").Trim(),
                            OpcaoMEI = fields[4].ToString().Replace("\"", "").Trim(),
                            DataOpcaoMEI = fields[5].ToString().Replace("\"", "").Trim(),
                            DataExclusaoMEI = fields[6].ToString().Replace("\"", "").Trim()
                        });

                        if (_count % 10000 == 0)
                        {
                            Console.Write($"  {_count}");
                            Console.Write("\r");
                        }
                    }

                    int _parts = 10;
                    int _size_parts = _lista_simples_completa.Count / _parts;

                    //quebra em 10 partes iguais
                    _groups = Enumerable
                                .Range(0, _parts)
                                .Select(s => _lista_simples_completa.Skip(s * _size_parts).Take(_size_parts))
                                .ToList();

                    //libera os itens para o coletor
                    //_lista_simples_completa.Clear();

                    //para cada grupo, execute o codigo;
                    foreach (var parts in _groups)
                    {
                        var _rows = 0;
                        Console.Write("\n|");

                        var dataTable = new DataTable();
                        dataTable.Columns.Add("CNPJBase");
                        dataTable.Columns.Add("OpcaoSimples");
                        dataTable.Columns.Add("DataOpcaoSimples");
                        dataTable.Columns.Add("DataExclusaoSimples");
                        dataTable.Columns.Add("OpcaoMEI");
                        dataTable.Columns.Add("DataOpcaoMEI");
                        dataTable.Columns.Add("DataExclusaoMEI");

                        foreach (var item in parts)
                        {
                            _rows++;
                            // Adicionar a linha à DataTable                            
                            DataRow row = dataTable.NewRow();
                            row["CNPJBase"] = item.CNPJBase;
                            row["OpcaoSimples"] = item.OpcaoSimples;
                            row["DataOpcaoSimples"] = item.DataOpcaoSimples;
                            row["DataExclusaoSimples"] = item.DataExclusaoSimples;
                            row["OpcaoMEI"] = item.OpcaoMEI;
                            row["DataOpcaoMEI"] = item.DataOpcaoMEI;
                            row["DataExclusaoMEI"] = item.DataExclusaoMEI;
                            dataTable.Rows.Add(row);

                            if (_rows % 100 == 0)
                            {
                                Console.Write($"  {_rows}");
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
                                // Mapear as colunas se necessário 
                                bulkCopy.ColumnMappings.Add("CNPJBase", "CNPJBase");
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
                            Log.Storage($"Lines: {_rows} | Migrated: {_rows} | Time: {_timer_migration.Elapsed:hh\\:mm\\:ss}");
                        }
                        dataTable.Dispose();
                    }
                }

            }
            _timer.Stop();
            Log.Storage($"TLines: {_count} | TMigrated: {_count} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    }

}