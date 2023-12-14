using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;
using migradata.Interfaces;
using System.Data;
using MongoDB.Driver.Linq;

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
            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".ESTABELE"))
            {
                var _data = Factory.Data(server);
                var _list = new List<MEstabelecimento>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    var _rows = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        var _uf = fields[19].ToString().Replace("\"", "").Trim();
                        var _cidade = fields[20].ToString().Replace("\"", "").Trim();
                        var _situacao = fields[5].ToString().Replace("\"", "").Trim();
                        /*
                        if (_uf == "SP" && _cidade != "7107" && (_situacao == "02" || _situacao == "08"))
                        {
                            _list.Add(DoFields(fields));
                            _rows++;
                            if (c1 % 1000 == 0)
                            {
                                Console.Write($"  {_rows}");
                                Console.Write("\r");
                            }
                        }*/

                        
                        foreach (var item in  MMunicipio.MicroRegionJau()
                                                        .Where(s => s == fields[20]
                                                        .ToString()
                                                        .Replace("\"", "")))
                        {

                            _list.Add(DoFields(fields));
                            _rows++;
                            if (c1 % 1000 == 0)
                            {
                                Console.Write($"  {_rows}");
                                Console.Write("\r");
                            }

                        }
                        c1++;
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MEstabelecimento>>();

                int parts = Cpu.Count;
                int size = (_list.Count / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Total: {_list.Count} -> Parts: {parts} -> Rows: {size}");
                Console.Write("\n|");

                foreach (var rows in _lists)
                {
                    _tasks.Add(Task.Run(async () =>
                    {
                        var _db = Factory.Data(server);
                        int registrosInseridos = 0;
                        int totalRegistros = size;
                        int progresso = 0;

                        foreach (var row in rows)
                        {
                            registrosInseridos++;
                            progresso = registrosInseridos * 100 / totalRegistros;
                            c2++;
                            await DoInsert(_insert, _db, row, database, datasource);

                            if (progresso % 10 == 0)
                            {
                                Console.Write($"| {progresso}% ");
                                Console.Write("\r");
                            }
                        }
                    }));
                }

                await Parallel.ForEachAsync(_tasks,
                    async (t, _) =>
                       await t
                    );

                //Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed:hh\\:mm\\:ss}");
            }
            _timer.Stop();
            // var db = Factory.Data(server);
            //var c3 = await db.ReadAsync(SqlCommands.SelectCommand("Estabelecimentos"), database, datasource);
            Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    });

    private static MEstabelecimento DoFields(string[] fields)
    => new()
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