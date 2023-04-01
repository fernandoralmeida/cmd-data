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
                    await new Log().Write($"Migrating File {Path.GetFileName(file)}");

                    var _data = Factory.Data(server);

                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            _data.ClearParameters();
                            _data.AddParameters("@Codigo", fields[0].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@Descricao", fields[1].ToString().Replace("\"", "").Trim());
                            await _data.WriteAsync(_insert);
                            i++;
                        }

                    _timer.Stop();
                    await new Log().Write($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
                }
                catch (Exception ex)
                {
                    await new Log().Write("Error: " + ex.Message);
                }
        });
}
