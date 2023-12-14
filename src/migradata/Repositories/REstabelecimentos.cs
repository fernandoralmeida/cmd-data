using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Interfaces;

namespace migradata.Repositories;

public static class REstabelecimentos
{

    public static async Task DoFileToDB(TServer server, string database, string datasource)
    {
        int c1 = 0;
        int c2 = 0;
        var _insert = SqlCommands.InsertCommand("Estabelecimentos",
                        SqlCommands.Fields_Estabelecimentos,
                        SqlCommands.Values_Estabelecimentos);

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await FilesCsv.FilesListAsync(@"C:\data", ".ESTABELE"))
            {
                var _timer_task = new Stopwatch();
                _timer_task.Start();
                var _data = Factory.Data(server);

                Log.Storage($"Reading File {Path.GetFileName(file)}");

                Console.Write("\n|");
                using var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1"));
                var _rows = 0;
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var fields = line!.Split(';');

                    var _uf = fields[19].ToString().Replace("\"", "").Trim();
                    var _cidade = fields[20].ToString().Replace("\"", "").Trim();

                    if (_uf == "SP" && _cidade != "7107")
                    {
                        _rows++;
                        await DoInsert(_insert, _data, fields, database, datasource);
                        if (c1 % 100000 == 0)
                        {
                            Console.Write($"  {_rows}");
                            Console.Write("\r");
                        }
                    }
                    c1++;
                }
                reader.Close();
                c2 += _rows;
                _timer_task.Stop();
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

    public static async Task DoFileToDBParallel(TServer server, string database, string datasource)
    {
        int c1 = 0;
        string _insert = SqlCommands.InsertCommand("Estabelecimentos",
                            SqlCommands.Fields_Estabelecimentos,
                            SqlCommands.Values_Estabelecimentos);

        var _data = Factory.Data(server);

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {
            //pega o nome de todos os arquivos tipo "X"
            var files = await FilesCsv.FilesListAsync(@"C:\data", ".ESTABELE");
            var _rows = 0;

            //limita o uso simultaneo de thread/core
            using var semaphore = new SemaphoreSlim(Cpu.Count <= 2 ? Cpu.Count : Cpu.Count - 1);

            var _tasks = new List<Task>();

            //percore todos os arquivos do diretório
            foreach (var file in files)
            {
                var _timer_file = new Stopwatch();
                _timer_file.Start();

                Log.Storage($"Reading File {Path.GetFileName(file)}");                
                //carrega o todas as linhas em memoria
                var lines = await File.ReadAllLinesAsync(file, Encoding.GetEncoding("ISO-8859-1"));
                Console.Write("\n|");
                //adiciona lista de tarefas em paralelo
                _tasks.AddRange(
                            lines
                            .Where(
                                    l => l.Split(';')[19].ToString().Replace("\"", "").Trim() == "SP"
                                    && l.Split(';')[20].ToString().Replace("\"", "").Trim() != "7107")
                            .Select(
                                line => Task.Run(async () =>
                                {
                                    await semaphore.WaitAsync();
                                    try
                                    {
                                        Interlocked.Increment(ref _rows);
                                        var fields = line.Split(';');
                                        await DoInsert(_insert, _data, fields, database, datasource);
                                        if (_rows % 1000 == 0)
                                        {
                                            Console.Write($"  {_rows}");
                                            Console.Write("\r");
                                        }
                                    }
                                    finally
                                    {
                                        semaphore.Release();
                                    }
                                }))
                            .ToList());

                c1 += lines.Length;
                _timer_file.Stop();
                Log.Storage($"Read : {c1} | Migrated: {_rows} | Time: {_timer_file.Elapsed:hh\\:mm\\:ss}");
            }
            await Task.WhenAll(_tasks);
            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {_rows} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");

        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    }

    private static async Task DoInsert(string sqlcommand, IData data, string[] fields, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CNPJOrdem", fields[1].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CNPJDV", fields[2].ToString().Replace("\"", "").Trim());
        data.AddParameters("@IdentificadorMatrizFilial", fields[3].ToString().Replace("\"", "").Trim());
        data.AddParameters("@NomeFantasia", fields[4].ToString().Replace("\"", "").Trim());
        data.AddParameters("@SituacaoCadastral", fields[5].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DataSituacaoCadastral", fields[6].ToString().Replace("\"", "").Trim());
        data.AddParameters("@MotivoSituacaoCadastral", fields[7].ToString().Replace("\"", "").Trim());
        data.AddParameters("@NomeCidadeExterior", fields[8].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Pais", fields[9].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DataInicioAtividade", fields[10].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CnaeFiscalPrincipal", fields[11].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CnaeFiscalSecundaria", fields[12].ToString().Replace("\"", "").Trim());
        data.AddParameters("@TipoLogradouro", fields[13].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Logradouro", fields[14].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Numero", fields[15].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Complemento", fields[16].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Bairro", fields[17].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CEP", fields[18].ToString().Replace("\"", "").Trim());
        data.AddParameters("@UF", fields[19].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Municipio", fields[20].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DDD1", fields[21].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Telefone1", fields[22].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DDD2", fields[23].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Telefone2", fields[24].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DDDFax", fields[25].ToString().Replace("\"", "").Trim());
        data.AddParameters("@Fax", fields[26].ToString().Replace("\"", "").Trim());
        data.AddParameters("@CorreioEletronico", fields[27].ToString().Replace("\"", "").Trim());
        data.AddParameters("@SituacaoEspecial", fields[28].ToString().Replace("\"", "").Trim());
        data.AddParameters("@DataSitucaoEspecial", fields[29].ToString().Replace("\"", "").Trim());
        await data.ExecuteAsync(sqlcommand, database, datasource);
    }

}