using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;

namespace migradata.Repositories;

public static class RSocios
{
    public static async Task DoFileToDB(TServer server, string database, string datasource)
    {
        int c1;
        int c2;
        int tc1 = 0;
        int tc2 = 0;
        var connectionString = $"{datasource}Database={database};";
        var tableName = "Socios";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".SOCIOCSV"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("IdentificadorSocio");
                dataTable.Columns.Add("NomeRazaoSocio");
                dataTable.Columns.Add("CnpjCpfSocio");
                dataTable.Columns.Add("QualificacaoSocio");
                dataTable.Columns.Add("DataEntradaSociedade");
                dataTable.Columns.Add("Pais");
                dataTable.Columns.Add("RepresentanteLegal");
                dataTable.Columns.Add("NomeRepresentante");
                dataTable.Columns.Add("QualificacaoRepresentanteLegal");
                dataTable.Columns.Add("FaixaEtaria");

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
                        row["IdentificadorSocio"] = fields[1].ToString().Replace("\"", "").Trim();
                        row["NomeRazaoSocio"] = fields[2].ToString().Replace("\"", "").Trim();
                        row["CnpjCpfSocio"] = fields[3].ToString().Replace("\"", "").Trim();
                        row["QualificacaoSocio"] = fields[4].ToString().Replace("\"", "").Trim();
                        row["DataEntradaSociedade"] = fields[5].ToString().Replace("\"", "").Trim();
                        row["Pais"] = fields[6].ToString().Replace("\"", "").Trim();
                        row["RepresentanteLegal"] = fields[7].ToString().Replace("\"", "").Trim();
                        row["NomeRepresentante"] = fields[8].ToString().Replace("\"", "").Trim();
                        row["QualificacaoRepresentanteLegal"] = fields[9].ToString().Replace("\"", "").Trim();
                        row["FaixaEtaria"] = fields[10].ToString().Replace("\"", "").Trim();

                        dataTable.Rows.Add(row);

                        c2++;

                        if (c2 % 100 == 0)
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
                            bulkCopy.ColumnMappings.Add("IdentificadorSocio", "IdentificadorSocio");
                            bulkCopy.ColumnMappings.Add("NomeRazaoSocio", "NomeRazaoSocio");
                            bulkCopy.ColumnMappings.Add("CnpjCpfSocio", "CnpjCpfSocio");
                            bulkCopy.ColumnMappings.Add("QualificacaoSocio", "QualificacaoSocio");
                            bulkCopy.ColumnMappings.Add("DataEntradaSociedade", "DataEntradaSociedade");
                            bulkCopy.ColumnMappings.Add("Pais", "Pais");
                            bulkCopy.ColumnMappings.Add("RepresentanteLegal", "RepresentanteLegal");
                            bulkCopy.ColumnMappings.Add("NomeRepresentante", "NomeRepresentante");
                            bulkCopy.ColumnMappings.Add("QualificacaoRepresentanteLegal", "QualificacaoRepresentanteLegal");
                            bulkCopy.ColumnMappings.Add("FaixaEtaria", "FaixaEtaria");

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