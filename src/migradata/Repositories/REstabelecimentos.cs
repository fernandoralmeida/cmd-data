using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;

namespace migradata.Repositories;

public class REstabelecimentos
{

    public static async Task DoFileToDBBulkCopy(TServer server, string database, string datasource)
    {
        int c1 = 0;
        int c2 = 0;
        int tc1 = 0;
        int tc2 = 0;

        var connectionString = $"{datasource}Database={database};";
        var tableName = "Estabelecimentos";

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".ESTABELE"))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("CNPJBase");
                dataTable.Columns.Add("CNPJOrdem");
                dataTable.Columns.Add("CNPJDV");
                dataTable.Columns.Add("IdentificadorMatrizFilial");
                dataTable.Columns.Add("NomeFantasia");
                dataTable.Columns.Add("SituacaoCadastral");
                dataTable.Columns.Add("DataSituacaoCadastral");
                dataTable.Columns.Add("MotivoSituacaoCadastral");
                dataTable.Columns.Add("NomeCidadeExterior");
                dataTable.Columns.Add("Pais");
                dataTable.Columns.Add("DataInicioAtividade");
                dataTable.Columns.Add("CnaeFiscalPrincipal");
                dataTable.Columns.Add("CnaeFiscalSecundaria");
                dataTable.Columns.Add("TipoLogradouro");
                dataTable.Columns.Add("Logradouro");
                dataTable.Columns.Add("Numero");
                dataTable.Columns.Add("Complemento");
                dataTable.Columns.Add("Bairro");
                dataTable.Columns.Add("CEP");
                dataTable.Columns.Add("UF");
                dataTable.Columns.Add("Municipio");
                dataTable.Columns.Add("DDD1");
                dataTable.Columns.Add("Telefone1");
                dataTable.Columns.Add("DDD2");
                dataTable.Columns.Add("Telefone2");
                dataTable.Columns.Add("DDDFax");
                dataTable.Columns.Add("Fax");
                dataTable.Columns.Add("CorreioEletronico");
                dataTable.Columns.Add("SituacaoEspecial");
                dataTable.Columns.Add("DataSitucaoEspecial");

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

                        var _uf = fields[19].ToString().Replace("\"", "").Trim();
                        var _cidade = fields[20].ToString().Replace("\"", "").Trim();
                        c1++;

                        if (_uf == "SP" && _cidade != "7107")
                        {
                            // Adicionar a linha à DataTable
                            DataRow row = dataTable.NewRow();
                            row["CNPJBase"] = fields[0].ToString().Replace("\"", "").Trim();
                            row["CNPJOrdem"] = fields[1].ToString().Replace("\"", "").Trim();
                            row["CNPJDV"] = fields[2].ToString().Replace("\"", "").Trim();
                            row["IdentificadorMatrizFilial"] = fields[3].ToString().Replace("\"", "").Trim();
                            row["NomeFantasia"] = fields[4].ToString().Replace("\"", "").Trim();
                            row["SituacaoCadastral"] = fields[5].ToString().Replace("\"", "").Trim();
                            row["DataSituacaoCadastral"] = fields[6].ToString().Replace("\"", "").Trim();
                            row["MotivoSituacaoCadastral"] = fields[7].ToString().Replace("\"", "").Trim();
                            row["NomeCidadeExterior"] = fields[8].ToString().Replace("\"", "").Trim();
                            row["Pais"] = fields[9].ToString().Replace("\"", "").Trim();
                            row["DataInicioAtividade"] = fields[10].ToString().Replace("\"", "").Trim();
                            row["CnaeFiscalPrincipal"] = fields[11].ToString().Replace("\"", "").Trim();
                            row["CnaeFiscalSecundaria"] = fields[12].ToString().Replace("\"", "").Trim();
                            row["TipoLogradouro"] = fields[13].ToString().Replace("\"", "").Trim();
                            row["Logradouro"] = fields[14].ToString().Replace("\"", "").Trim();
                            row["Numero"] = fields[15].ToString().Replace("\"", "").Trim();
                            row["Complemento"] = fields[16].ToString().Replace("\"", "").Trim();
                            row["Bairro"] = fields[17].ToString().Replace("\"", "").Trim();
                            row["CEP"] = fields[18].ToString().Replace("\"", "").Trim();
                            row["UF"] = fields[19].ToString().Replace("\"", "").Trim();
                            row["Municipio"] = fields[20].ToString().Replace("\"", "").Trim();
                            row["DDD1"] = fields[21].ToString().Replace("\"", "").Trim();
                            row["Telefone1"] = fields[22].ToString().Replace("\"", "").Trim();
                            row["DDD2"] = fields[23].ToString().Replace("\"", "").Trim();
                            row["Telefone2"] = fields[24].ToString().Replace("\"", "").Trim();
                            row["DDDFax"] = fields[25].ToString().Replace("\"", "").Trim();
                            row["Fax"] = fields[26].ToString().Replace("\"", "").Trim();
                            row["CorreioEletronico"] = fields[27].ToString().Replace("\"", "").Trim();
                            row["SituacaoEspecial"] = fields[28].ToString().Replace("\"", "").Trim();
                            row["DataSitucaoEspecial"] = fields[29].ToString().Replace("\"", "").Trim();

                            dataTable.Rows.Add(row);

                            c2++;

                            if (c2 % 100 == 0)
                            {
                                Console.Write($"  {c2}");
                                Console.Write("\r");
                            }
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
                            // Mapear as colunas se necessário (se a ordem das colunas na DataTable não coincidir com a ordem na tabela do banco de dados)
                            bulkCopy.ColumnMappings.Add("CNPJBase", "CNPJBase");
                            bulkCopy.ColumnMappings.Add("CNPJOrdem", "CNPJOrdem");
                            bulkCopy.ColumnMappings.Add("CNPJDV", "CNPJDV");
                            bulkCopy.ColumnMappings.Add("IdentificadorMatrizFilial", "IdentificadorMatrizFilial");
                            bulkCopy.ColumnMappings.Add("NomeFantasia", "NomeFantasia");
                            bulkCopy.ColumnMappings.Add("SituacaoCadastral", "SituacaoCadastral");
                            bulkCopy.ColumnMappings.Add("DataSituacaoCadastral", "DataSituacaoCadastral");
                            bulkCopy.ColumnMappings.Add("MotivoSituacaoCadastral", "MotivoSituacaoCadastral");
                            bulkCopy.ColumnMappings.Add("NomeCidadeExterior", "NomeCidadeExterior");
                            bulkCopy.ColumnMappings.Add("Pais", "Pais");
                            bulkCopy.ColumnMappings.Add("DataInicioAtividade", "DataInicioAtividade");
                            bulkCopy.ColumnMappings.Add("CnaeFiscalPrincipal", "CnaeFiscalPrincipal");
                            bulkCopy.ColumnMappings.Add("CnaeFiscalSecundaria", "CnaeFiscalSecundaria");
                            bulkCopy.ColumnMappings.Add("TipoLogradouro", "TipoLogradouro");
                            bulkCopy.ColumnMappings.Add("Logradouro", "Logradouro");
                            bulkCopy.ColumnMappings.Add("Numero", "Numero");
                            bulkCopy.ColumnMappings.Add("Complemento", "Complemento");
                            bulkCopy.ColumnMappings.Add("Bairro", "Bairro");
                            bulkCopy.ColumnMappings.Add("CEP", "CEP");
                            bulkCopy.ColumnMappings.Add("UF", "UF");
                            bulkCopy.ColumnMappings.Add("Municipio", "Municipio");
                            bulkCopy.ColumnMappings.Add("DDD1", "DDD1");
                            bulkCopy.ColumnMappings.Add("Telefone1", "Telefone1");
                            bulkCopy.ColumnMappings.Add("DDD2", "DDD2");
                            bulkCopy.ColumnMappings.Add("Telefone2", "Telefone2");
                            bulkCopy.ColumnMappings.Add("DDDFax", "DDDFax");
                            bulkCopy.ColumnMappings.Add("Fax", "Fax");
                            bulkCopy.ColumnMappings.Add("CorreioEletronico", "CorreioEletronico");
                            bulkCopy.ColumnMappings.Add("SituacaoEspecial", "SituacaoEspecial");
                            bulkCopy.ColumnMappings.Add("DataSitucaoEspecial", "DataSitucaoEspecial");

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