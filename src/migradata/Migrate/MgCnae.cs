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

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".CNAECSV"))
                try
                {
                    Log.Storage($"Migrating File {Path.GetFileName(file)}");

                    var _data = Factory.Data(server);

                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            _data.ClearParameters();
                            _data.AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                            await _data.WriteAsync(_insert, database, datasource);
                            i++;
                        }

                    _timer.Stop();
                    Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }
        });

    public static async Task DatabaseToDataBaseAsync(TServer server, string databaseRead, string datasourceRead, string databaseWrite, string datasourceWrite)
    => await Task.Run(async () =>
    {
        var _timer = new Stopwatch();
        _timer.Start();

        int i = 0;
        var _select = SqlCommands.SelectCommand("Cnaes");
        var _insert = SqlCommands.InsertCommand("Cnaes", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

        var _sqlserver = Factory.Data(TServer.SqlServer);

        var _dataVPS = Factory.Data(server);

        foreach (DataRow row in _sqlserver.ReadAsync(_select, databaseRead, datasourceRead).Result.Rows)
            try
            {
                _dataVPS.ClearParameters();
                _dataVPS.AddParameters("@Codigo", row[0]);
                _dataVPS.AddParameters("@Descricao", row[1]);
                await _dataVPS.WriteAsync(_insert, databaseWrite, datasourceWrite);
                i++;
            }
            catch (Exception ex)
            {
                Log.Storage("Error: " + ex.Message);
            }

        _timer.Stop();
        Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
    });
}