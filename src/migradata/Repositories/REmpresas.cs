using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;

namespace migradata.Repositories;

public static class REmpresas
{
    public static async Task DoFileToDB(TServer server, string database, string datasource)
    {
        int c1 = 0;
        int c2 = 0;
        var _insert = SqlCommands.InsertCommand("Empresas",
                        SqlCommands.Fields_Empresas,
                        SqlCommands.Values_Empresas);

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".EMPRECSV"))
            {
                var _data = Factory.Data(server);
                var _timer_task = new Stopwatch();
                _timer_task.Start();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");
                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                var _rows = 0;
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var fields = line!.Split(';');

                    await DoInsert(_insert, _data, fields, database, datasource);
                    _rows++;
                    if (c1 % 100000 == 0)
                    {
                        Console.Write($"  {_rows}");
                        Console.Write("\r");
                    }

                    c1++;
                }
                reader.Close();
                c2 += _rows;
                _timer_task.Start();
                Log.Storage($"Read: {c1} | Migrated: {_rows} | Time: {_timer_task.Elapsed:hh\\:mm\\:ss}");
            }

            _timer.Stop();

            Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    }

    private static async Task DoInsert(string sqlcommand, IData data, string[] fields, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", ""));
        data.AddParameters("@RazaoSocial", fields[1].ToString().Replace("\"", ""));
        data.AddParameters("@NaturezaJuridica", fields[2].ToString().Replace("\"", "").Length <= 4 ? fields[2].ToString().Replace("\"", "") : "0000");
        data.AddParameters("@QualificacaoResponsavel", fields[3].ToString().Replace("\"", "").Length <= 2 ? fields[3].ToString().Replace("\"", "") : "00");
        data.AddParameters("@CapitalSocial", fields[4].ToString().Replace("\"", "")!);
        data.AddParameters("@PorteEmpresa", fields[5].ToString().Replace("\"", "").Length <= 2 ? fields[5].ToString().Replace("\"", "") : "00");
        data.AddParameters("@EnteFederativoResponsavel", fields[6].ToString().Replace("\"", ""));
        await data.ExecuteAsync(sqlcommand, database, datasource);
    }

}