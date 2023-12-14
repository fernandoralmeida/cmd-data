using System.Data;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;

namespace migradata.Repositories;

public static class RNatureza
{
    public static async Task DoFileToDB(TServer server, string database, string datasource)
    {
        int i = 0;
        var _insert = SqlCommands.InsertCommand("NaturezaJuridica", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);
        var _timer = new Stopwatch();
        _timer.Start();

        foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".NATJUCSV"))
            try
            {
                Log.Storage($"Migrating File {Path.GetFileName(file)}");

                var _data = Factory.Data(server);
                var _dtable = new DataTable();

                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var fields = line!.Split(';');
                        _data.ClearParameters();
                        _data.AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                        _data.AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                        await _data.ExecuteAsync(_insert, database, datasource);
                        i++;
                    }

                await _data.WriteAsync(_dtable, "NaturezaJuridica", database, datasource);
                _timer.Stop();
                Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
            }
            catch (Exception ex)
            {
                Log.Storage("Error: " + ex.Message);
            }
    }
}