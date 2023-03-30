using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Repositories;
using migradata.Models;

namespace migradata.Migrate;

public static class Estabelecimentos
{
    public static async Task StartAsync()
    => await Task.Run(async () =>
    {
        int i = 0;
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

                var T1 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list1)
                    {
                        await DoList(_insert, _data, _list1);
                        c1++;
                        Console.WriteLine($"{c1}");
                    }
                });

                var T2 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list2)
                    {
                        await DoList(_insert, _data, _list2);
                        c2++;
                        Console.WriteLine($"{c2}");
                    }
                });

                var T3 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list3)
                    {
                        await DoList(_insert, _data, _list3);
                        c3++;
                        Console.WriteLine($"{c3}");
                    }
                });

                var T4 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list4)
                    {
                        await DoList(_insert, _data, _list4);
                        c4++;
                        Console.WriteLine($"{c4}");
                    }
                });

                var T5 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list5)
                    {
                        await DoList(_insert, _data, _list5);
                        c5++;
                        Console.WriteLine($"{c5}");
                    }
                });

                var T6 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list6)
                    {
                        await DoList(_insert, _data, _list6);
                        c6++;
                        Console.WriteLine($"{c6}");
                    }
                });

                var T7 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list7)
                    {
                        await DoList(_insert, _data, _list7);
                        c6++;
                        Console.WriteLine($"{c7}");
                    }
                });

                var T8 = Task.Run(async () =>
                {
                    var _data = new Generic();
                    foreach (var item in _list8)
                    {
                        await DoList(_insert, _data, _list8);
                        c8++;
                        Console.WriteLine($"{c8}");
                    }
                });

                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);

                i++;

            }
            _timer.Stop();
            Console.WriteLine($"Registros percorridos {i}, migrados: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
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
    private static async Task DoList(string sqlcommand, Generic data, IEnumerable<Estabelecimento> list)
    {
        foreach (var item in list)
        {
            data.ClearParameters();
            data.AddParameters("@CNPJBase", item.CNPJBase!);
            data.AddParameters("@CNPJOrdem", item.CNPJOrdem!);
            data.AddParameters("@CNPJDV", item.CNPJDV!);
            data.AddParameters("@IdentificadorMatrizFilial", item.IdentificadorMatrizFilial!);
            data.AddParameters("@NomeFantasia", item.NomeFantasia!);
            data.AddParameters("@SituacaoCadastral", item.SituacaoCadastral!);
            data.AddParameters("@DataSituacaoCadastral", item.DataSituacaoCadastral!);
            data.AddParameters("@MotivoSituacaoCadastral", item.MotivoSituacaoCadastral!);
            data.AddParameters("@NomeCidadeExterior", item.NomeCidadeExterior!);
            data.AddParameters("@Pais", item.Pais!);
            data.AddParameters("@DataInicioAtividade", item.DataInicioAtividade!);
            data.AddParameters("@CnaeFiscalPrincipal", item.CnaeFiscalPrincipal!);
            data.AddParameters("@CnaeFiscalSecundaria", item.CnaeFiscalSecundaria!);
            data.AddParameters("@TipoLogradouro", item.TipoLogradouro!);
            data.AddParameters("@Logradouro", item.Logradouro!);
            data.AddParameters("@Numero", item.Numero!);
            data.AddParameters("@Complemento", item.Complemento!);
            data.AddParameters("@Bairro", item.Bairro!);
            data.AddParameters("@CEP", item.CEP!);
            data.AddParameters("@UF", item.UF!);
            data.AddParameters("@Municipio", item.Municipio!);
            data.AddParameters("@DDD1", item.DDD1!);
            data.AddParameters("@Telefone1", item.Telefone1!);
            data.AddParameters("@DDD2", item.DDD2!);
            data.AddParameters("@Telefone2", item.Telefone2!);
            data.AddParameters("@DDDFax", item.DDDFax!);
            data.AddParameters("@Fax", item.Fax!);
            data.AddParameters("@CorreioEletronico", item.CorreioEletronico!.ToLower());
            data.AddParameters("@SituacaoEspecial", item.SituacaoEspecial!);
            data.AddParameters("@DataSitucaoEspecial", item.DataSitucaoEspecial!);
            await Task.Run(() => {});// data.WriteAsync(sqlcommand);
        }
    }

}