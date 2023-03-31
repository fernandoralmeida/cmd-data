using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Repositories;

namespace migradata.Migrate;

public static class MgCnaes
{
    public static async Task StartAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _insert = SqlCommands.InsertCommand("Cnaes", SqlCommands.Fields_Generic, SqlCommands.Values_Generic);

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".CNAECSV"))
                try
                {
                    Console.WriteLine($"Migration File {Path.GetFileName(file)}");

                    var _data = new Generic();

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
                    Console.WriteLine($"Read: {i}, migrated: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });
}