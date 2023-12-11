using System.Data;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;

namespace migradata.Migrate;

public static class MgCnaes
{
    public static async Task FileToDataBase(TServer server, string database, string datasource)
        => await Task.Run(async () =>
        {
            int i = 0;
            var _insert = SqlCommands.InsertCommand("Cnaes", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".CNAECSV"))
                try
                {
                    Log.Storage($"Migrating File {Path.GetFileName(file)}");

                    var _data = Factory.Data(server);

                    var _dtable = new DataTable();

                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            _dtable.Rows.Add(DoDataTable(_dtable, fields));
                            i++;
                        }

                    await _data.WriteAsync(_dtable, "Cnaes", database, datasource);
                    _timer.Stop();
                    Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }
        });

    private static DataRow DoDataTable(DataTable dataTable, string[] fields)
    {
        var row = dataTable.NewRow();
        row["Codigo"] = fields[0].ToString().Replace("\"", "").Trim();
        row["Descricao"] = fields[1].ToString().Replace("\"", "").Trim();
        return row;
    }
}