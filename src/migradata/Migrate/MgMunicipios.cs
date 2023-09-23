using System.Data;
using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.SqlServer;

namespace migradata.Migrate;

public static class MgMunicipios
{
    public static async Task StartAsync(TServer server)
        => await Task.Run(async () =>
        {
            int i = 0;

            var _insert = SqlCommands.InsertCommand("Municipios", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".MUNICCSV"))
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
                            await _data.WriteAsync(_insert, DataBase.MigraData_RFB);
                            i++;
                        }

                    _timer.Stop();
                    Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }
        });

    public static async Task ToVpsAsync(TServer server)
        => await Task.Run(async () =>
        {
            var _timer = new Stopwatch();
            _timer.Start();

            int i = 0;
            var _select = SqlCommands.SelectCommand("Municipios");
            var _insert = SqlCommands.InsertCommand("Municipios", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

            var _sqlserver = Factory.Data(TServer.SqlServer);

            var _dataVPS = Factory.Data(server);

            foreach (DataRow row in _sqlserver.ReadAsync(_select, DataBase.Sim_RFB_db20210001).Result.Rows)
                try
                {
                    _dataVPS.ClearParameters();
                    _dataVPS.AddParameters("@Codigo", row[0]);
                    _dataVPS.AddParameters("@Descricao", row[1]);
                    await _dataVPS.WriteAsync(_insert, DataBase.IndicadoresNET);
                    i++;
                    Console.Write(i);
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }

            _timer.Stop();
            Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        });
}
