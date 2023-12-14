using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;

namespace migradata.Repositories;

public static class RSimples
{
    public static async Task DoFileToDB(TServer server, string database, string datasource)
    {
        int c1;
        int c2;
        int tc1 = 0;
        int tc2 = 0;
        var connectionString = $"{datasource}Database={database};";
        var tableName = "Simples";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".D3"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("OpcaoSimples");
                dataTable.Columns.Add("DataOpcaoSimples");
                dataTable.Columns.Add("DataExclusaoSimples");
                dataTable.Columns.Add("OpcaoMEI");
                dataTable.Columns.Add("DataOpcaoMEI");
                dataTable.Columns.Add("DataExclusaoMEI");

                c2 = 0;
                c1 = 0;

                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");

                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');

                        c1++;

                        // Adicionar a linha à DataTable
                        DataRow row = dataTable.NewRow();
                        row["CNPJBase"] = fields[0].ToString().Replace("\"", "").Trim();
                        row["OpcaoSimples"] = fields[1].ToString().Replace("\"", "").Trim();
                        row["DataOpcaoSimples"] = fields[2].ToString().Replace("\"", "").Trim();
                        row["DataExclusaoSimples"] = fields[3].ToString().Replace("\"", "").Trim();
                        row["OpcaoMEI"] = fields[4].ToString().Replace("\"", "").Trim();
                        row["DataOpcaoMEI"] = fields[5].ToString().Replace("\"", "").Trim();
                        row["DataExclusaoMEI"] = fields[6].ToString().Replace("\"", "").Trim();

                        dataTable.Rows.Add(row);

                        c2++;

                        if (c2 % 10000 == 0)
                        {
                            Console.Write($"  {c2}");
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
                        Log.Storage($"Lines: {c1} | Migrated: {c2} | Time: {_timer_migration.Elapsed:hh\\:mm\\:ss}");
                    }

                }
                tc1 += c1;
                tc2 += c2;
                dataTable.Dispose();
            }
            _timer.Stop();
            Log.Storage($"TLines: {tc1} | TMigrated: {tc2} | TTime: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    }

}