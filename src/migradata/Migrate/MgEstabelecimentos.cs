using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.SqlServer;
using migradata.Models;
using migradata.Interfaces;
using System.Data;

namespace migradata.Migrate;

public static class MgEstabelecimentos
{
    public static async Task FileToDataBase(TServer server, string database, string datasource)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;

        var _insert = SqlCommands.InsertCommand("Estabelecimentos",
                        SqlCommands.Fields_Estabelecimentos,
                        SqlCommands.Values_Estabelecumentos);

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".ESTABELE"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _data = Factory.Data(server);
                var _list = new List<MEstabelecimento>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        foreach (var item in MMunicipio.MicroRegionJau()
                                                        .Where(s => s == fields[20]
                                                        .ToString()
                                                        .Replace("\"", "")))
                            _list.Add(DoFields(fields));

                        c1++;
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MEstabelecimento>>();

                int parts = Cpu.Count;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count()} -> {parts} : {size}");

                foreach (var rows in _lists)
                    _tasks.Add(new Task(async () =>
                    {
                        var i = 0;
                        var _db = Factory.Data(server);
                        foreach (var row in rows)
                        {
                            i++;
                            await DoInsert(_insert, _db, row, database, datasource);
                        }
                        c2 += i;
                    }));

                Parallel.ForEach(_tasks, t => t.Start());

                _innertimer.Stop();

                Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed:hh\\:mm\\:ss}");
            }
            _timer.Stop();
            var db = Factory.Data(server);
            var c3 = await db.ReadAsync(SqlCommands.SelectCommand("Estabelecimentos"), database, datasource);
            Log.Storage($"Read: {c1} | Migrated: {c3.Rows.Count} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    });

    public static async Task DatabaseToDataBaseAsync(TServer server, string databaseRead, string datasourceRead, string databaseWrite, string datasourceWrite)
    => await Task.Run(async () =>
    {

        var _timer = new Stopwatch();
        _timer.Start();

        int i = 0;
        var _select = SqlCommands.SelectCommand("Estabelecimentos");
        var _insert = SqlCommands.InsertCommand("Estabelecimentos", SqlCommands.Fields_Estabelecimentos, SqlCommands.Values_Estabelecumentos);

        var _sqlserver = Factory.Data(TServer.SqlServer);

        var _dataVPS = Factory.Data(server);

        foreach (DataRow row in _sqlserver.ReadAsync(_select, databaseRead, datasourceRead).Result.Rows)
            try
            {
                _dataVPS.ClearParameters();
                _dataVPS.AddParameters("@CNPJBase", row[0]);
                _dataVPS.AddParameters("@CNPJOrdem", row[1]);
                _dataVPS.AddParameters("@CNPJDV", row[2]);
                _dataVPS.AddParameters("@IdentificadorMatrizFilial", row[3]);
                _dataVPS.AddParameters("@NomeFantasia", row[4]);
                _dataVPS.AddParameters("@SituacaoCadastral", row[5]);
                _dataVPS.AddParameters("@DataSituacaoCadastral", row[6]);
                _dataVPS.AddParameters("@MotivoSituacaoCadastral", row[7]);
                _dataVPS.AddParameters("@NomeCidadeExterior", row[8]);
                _dataVPS.AddParameters("@Pais", row[9]);
                _dataVPS.AddParameters("@DataInicioAtividade", row[10]);
                _dataVPS.AddParameters("@CnaeFiscalPrincipal", row[11]);
                _dataVPS.AddParameters("@CnaeFiscalSecundaria", row[12]);
                _dataVPS.AddParameters("@TipoLogradouro", row[13]);
                _dataVPS.AddParameters("@Logradouro", row[14]);
                _dataVPS.AddParameters("@Numero", row[15]);
                _dataVPS.AddParameters("@Complemento", row[16]);
                _dataVPS.AddParameters("@Bairro", row[17]);
                _dataVPS.AddParameters("@CEP", row[18]);
                _dataVPS.AddParameters("@UF", row[19]);
                _dataVPS.AddParameters("@Municipio", row[20]);
                _dataVPS.AddParameters("@DDD1", row[21]);
                _dataVPS.AddParameters("@Telefone1", row[22]);
                _dataVPS.AddParameters("@DDD2", row[23]);
                _dataVPS.AddParameters("@Telefone2", row[24]);
                _dataVPS.AddParameters("@DDDFax", row[25]);
                _dataVPS.AddParameters("@Fax", row[26]);
                _dataVPS.AddParameters("@CorreioEletronico", row[27]);
                _dataVPS.AddParameters("@SituacaoEspecial", row[28]);
                _dataVPS.AddParameters("@DataSitucaoEspecial", row[29]);
                await _dataVPS.WriteAsync(_insert, databaseWrite, datasourceWrite);
                i++;
                Console.Write(i);
            }
            catch (Exception ex)
            {
                Log.Storage("Error: " + ex.Message);
            }

        _timer.Stop();
        Log.Storage($"Read: {i} | Migrated: {i} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
    });

    private static MEstabelecimento DoFields(string[] fields)
    => new MEstabelecimento()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        CNPJOrdem = fields[1].ToString().Replace("\"", "").Trim(),
        CNPJDV = fields[2].ToString().Replace("\"", "").Trim(),
        IdentificadorMatrizFilial = fields[3].ToString().Replace("\"", "").Trim(),
        NomeFantasia = fields[4].ToString().Replace("\"", "").Trim(),
        SituacaoCadastral = fields[5].ToString().Replace("\"", "").Trim(),
        DataSituacaoCadastral = fields[6].ToString().Replace("\"", "").Trim(),
        MotivoSituacaoCadastral = fields[7].ToString().Replace("\"", "").Trim(),
        NomeCidadeExterior = fields[8].ToString().Replace("\"", "").Trim(),
        Pais = fields[9].ToString().Replace("\"", "").Trim(),
        DataInicioAtividade = fields[10].ToString().Replace("\"", "").Trim(),
        CnaeFiscalPrincipal = fields[11].ToString().Replace("\"", "").Trim(),
        CnaeFiscalSecundaria = fields[12].ToString().Replace("\"", "").Trim(),
        TipoLogradouro = fields[13].ToString().Replace("\"", "").Trim(),
        Logradouro = fields[14].ToString().Replace("\"", "").Trim(),
        Numero = fields[15].ToString().Replace("\"", "").Trim(),
        Complemento = fields[16].ToString().Replace("\"", "").Trim(),
        Bairro = fields[17].ToString().Replace("\"", "").Trim(),
        CEP = fields[18].ToString().Replace("\"", "").Trim(),
        UF = fields[19].ToString().Replace("\"", "").Trim(),
        Municipio = fields[20].ToString().Replace("\"", "").Trim(),
        DDD1 = fields[21].ToString().Replace("\"", "").Trim(),
        Telefone1 = fields[22].ToString().Replace("\"", "").Trim(),
        DDD2 = fields[23].ToString().Replace("\"", "").Trim(),
        Telefone2 = fields[24].ToString().Replace("\"", "").Trim(),
        DDDFax = fields[25].ToString().Replace("\"", "").Trim(),
        Fax = fields[26].ToString().Replace("\"", "").Trim(),
        CorreioEletronico = fields[27].ToString().Replace("\"", "").Trim(),
        SituacaoEspecial = fields[28].ToString().Replace("\"", "").Trim(),
        DataSitucaoEspecial = fields[29].ToString().Replace("\"", "").Trim()
    };

    private static async Task DoInsert(string sqlcommand, IData data, MEstabelecimento est, string database, string datasource)
    {
        data.ClearParameters();
        data.AddParameters("@CNPJBase", est.CNPJBase!);
        data.AddParameters("@CNPJOrdem", est.CNPJOrdem!);
        data.AddParameters("@CNPJDV", est.CNPJDV!);
        data.AddParameters("@IdentificadorMatrizFilial", est.IdentificadorMatrizFilial!);
        data.AddParameters("@NomeFantasia", est.NomeFantasia!);
        data.AddParameters("@SituacaoCadastral", est.SituacaoCadastral!);
        data.AddParameters("@DataSituacaoCadastral", est.DataSituacaoCadastral!);
        data.AddParameters("@MotivoSituacaoCadastral", est.MotivoSituacaoCadastral!);
        data.AddParameters("@NomeCidadeExterior", est.NomeCidadeExterior!);
        data.AddParameters("@Pais", est.Pais!);
        data.AddParameters("@DataInicioAtividade", est.DataInicioAtividade!);
        data.AddParameters("@CnaeFiscalPrincipal", est.CnaeFiscalPrincipal!);
        data.AddParameters("@CnaeFiscalSecundaria", est.CnaeFiscalSecundaria!);
        data.AddParameters("@TipoLogradouro", est.TipoLogradouro!);
        data.AddParameters("@Logradouro", est.Logradouro!);
        data.AddParameters("@Numero", est.Numero!);
        data.AddParameters("@Complemento", est.Complemento!);
        data.AddParameters("@Bairro", est.Bairro!);
        data.AddParameters("@CEP", est.CEP!);
        data.AddParameters("@UF", est.UF!);
        data.AddParameters("@Municipio", est.Municipio!);
        data.AddParameters("@DDD1", est.DDD1!);
        data.AddParameters("@Telefone1", est.Telefone1!);
        data.AddParameters("@DDD2", est.DDD2!);
        data.AddParameters("@Telefone2", est.Telefone2!);
        data.AddParameters("@DDDFax", est.DDDFax!);
        data.AddParameters("@Fax", est.Fax!);
        data.AddParameters("@CorreioEletronico", est.CorreioEletronico!.ToLower());
        data.AddParameters("@SituacaoEspecial", est.SituacaoEspecial!);
        data.AddParameters("@DataSitucaoEspecial", est.DataSitucaoEspecial!);
        await data.WriteAsync(sqlcommand, database, datasource);
    }

}