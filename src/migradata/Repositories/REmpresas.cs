using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;

namespace migradata.Repositories;

public static class REmpresas
{
    public static async Task DoFileToDBBulkCopy(TServer server, string database, string datasource)
    {
        int c1;
        int c2;
        int tc1 = 0;
        int tc2 = 0;

        var connectionString = $"{datasource}Database={database};";
        var tableName = "Empresas";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".EMPRECSV"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("RazaoSocial");
                dataTable.Columns.Add("NaturezaJuridica");
                dataTable.Columns.Add("QualificacaoResponsavel");
                dataTable.Columns.Add("CapitalSocial");
                dataTable.Columns.Add("PorteEmpresa");
                dataTable.Columns.Add("EnteFederativoResponsavel");

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
                        row["RazaoSocial"] = fields[1].ToString().Replace("\"", "").Trim();
                        row["NaturezaJuridica"] = fields[2].ToString().Replace("\"", "").Length <= 4 ? fields[2].ToString().Replace("\"", "").Trim() : "0000";
                        row["QualificacaoResponsavel"] = fields[3].ToString().Replace("\"", "").Length <= 2 ? fields[3].ToString().Replace("\"", "").Trim() : "00";
                        row["CapitalSocial"] = fields[4].ToString().Replace("\"", "").Trim()!;
                        row["PorteEmpresa"] = fields[5].ToString().Replace("\"", "").Length <= 2 ? fields[5].ToString().Replace("\"", "").Trim() : "00";
                        row["EnteFederativoResponsavel"] = fields[6].ToString().Replace("\"", "").Trim();

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
                            bulkCopy.ColumnMappings.Add("RazaoSocial", "RazaoSocial");
                            bulkCopy.ColumnMappings.Add("NaturezaJuridica", "NaturezaJuridica");
                            bulkCopy.ColumnMappings.Add("QualificacaoResponsavel", "QualificacaoResponsavel");
                            bulkCopy.ColumnMappings.Add("CapitalSocial", "CapitalSocial");
                            bulkCopy.ColumnMappings.Add("PorteEmpresa", "PorteEmpresa");
                            bulkCopy.ColumnMappings.Add("EnteFederativoResponsavel", "EnteFederativoResponsavel");

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