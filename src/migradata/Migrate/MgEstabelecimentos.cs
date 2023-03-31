using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Repositories;
using migradata.Models;

namespace migradata.Migrate;

public static class MgEstabelecimentos
{
    public static async Task StartAsync()
    => await Task.Run(async () =>
    {
        int i = 0;
        int f = 0;
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        int c4 = 0;
        int c5 = 0;
        int c6 = 0;
        int c7 = 0;
        int c8 = 0;

        var _insert = SqlCommands.InsertCommand("Estabelecimentos",
                        SqlCommands.Fields_Estabelecimentos,
                        SqlCommands.Values_Estabelecumentos);

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".ESTABELE"))
            {
                var _data = new Generic();
                var _list = new List<Estabelecimento>();
                Console.WriteLine($"Migration File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        foreach (var item in Conditional.MicroRegionJau()
                                                        .Where(s => s == fields[20]
                                                        .ToString()
                                                        .Replace("\"", "")))
                            _list.Add(DoFields(fields));

                        i++;
                    }
                }

                int parts = 8;
                int size = (_list.Count() / parts) + 1;
                int _end = (size * 8) - _list.Count();
                var _list1 = _list.Skip(0 * size).Take(size);
                var _list2 = _list.Skip(1 * size).Take(size);
                var _list3 = _list.Skip(2 * size).Take(size);
                var _list4 = _list.Skip(3 * size).Take(size);
                var _list5 = _list.Skip(4 * size).Take(size);
                var _list6 = _list.Skip(5 * size).Take(size);
                var _list7 = _list.Skip(6 * size).Take(size);
                var _list8 = _list.Skip(7 * size).Take(size);

                Console.WriteLine($"Reading: {_list.Count()} -> {parts} : {size}");

                var T1 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list1)
                        await DoList(_insert, _data, item, c1++);

                });

                var T2 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list2)
                        await DoList(_insert, _data, item, c2++);
                });

                var T3 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list3)
                        await DoList(_insert, _data, item, c3++);
                });

                var T4 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list4)
                        await DoList(_insert, _data, item, c4++);
                });

                var T5 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list5)
                        await DoList(_insert, _data, item, c5++);
                });

                var T6 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list6)
                        await DoList(_insert, _data, item, c6++);
                });

                var T7 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list7)
                        await DoList(_insert, _data, item, c7++);
                });

                var T8 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list8)
                        await DoList(_insert, _data, item, c8++);
                });

                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                f += c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8;
                Console.WriteLine($"Read: {i}, migrated: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
            }
            _timer.Stop();
            Console.WriteLine($"Read: {i}, migrated: {f}, {_timer.Elapsed.TotalMinutes} minutes");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    });
    private static Estabelecimento DoFields(string[] fields)
    => new Estabelecimento()
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
        CorreioEletronico = fields[28].ToString().Replace("\"", "").Trim(),
        DataSitucaoEspecial = fields[29].ToString().Replace("\"", "").Trim()
    };
    
    private static async Task DoList(string sqlcommand, Generic data, Estabelecimento est, int cont)
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
        await data.WriteAsync(sqlcommand);
    }

}