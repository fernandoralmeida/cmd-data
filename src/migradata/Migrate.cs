using System.Diagnostics;
using System.Text;
using migradata.Models;
using migradata.Repositories;
using migradata.Helpers;

namespace migradata;

public class Migratesss
{

    public async Task Normalize()
    {
        var data = new Generic();
        Console.WriteLine("Verificando Conexões...");
        Thread.Sleep(3000);
        data.CheckDB();
        Thread.Sleep(3000);
        Console.WriteLine("Normalizando Banco de Dados...");
        await data.WriteAsync(@"DELETE FROM Cnaes");
        Console.WriteLine("Tebela Cnaes Normalizado!");
        await data.WriteAsync(@"DELETE FROM MotivoSituacaoCadastral");
        Console.WriteLine("Tebela MotivoSituacaoCadastral Normalizado!");
        await data.WriteAsync(@"DELETE FROM Municipios");
        Console.WriteLine("Tebela Municipios Normalizado!");
        await data.WriteAsync(@"DELETE FROM NaturezaJuridica");
        Console.WriteLine("Tebela NaturezaJuridica Normalizado!");
        await data.WriteAsync(@"DELETE FROM Paises");
        Console.WriteLine("Tebela Paises Normalizado!");
        await data.WriteAsync(@"DELETE FROM QualificacaoSocios");
        Console.WriteLine("Tebela QualificacaoSocios Normalizado!");
        await data.WriteAsync(@"DELETE FROM Estabelecimentos");
        Console.WriteLine("Tebela Estabelecimentos Normalizado!");
        await data.WriteAsync(@"DELETE FROM Empresas");
        Console.WriteLine("Tebela Empresas Normalizado!");
        await data.WriteAsync(@"DELETE FROM Socios");
        Console.WriteLine("Tebela Socios Normalizado!");
        await data.WriteAsync(@"DELETE FROM Simples");
        Console.WriteLine("Tebela Simples Normalizado!");
        Thread.Sleep(3000);
    }
    public async Task EstabelecimentosAsyn()
        => await Task.Run(async () =>
            {
                int i = 0;
                int f = 0;
                var _insertFields = @"(CNPJBase,
                                                CNPJOrdem,
                                                CNPJDV,
                                                IdentificadorMatrizFilial,
                                                NomeFantasia,
                                                SituacaoCadastral,
                                                DataSituacaoCadastral,
                                                MotivoSituacaoCadastral,
                                                NomeCidadeExterior,
                                                Pais,
                                                DataInicioAtividade,
                                                CnaeFiscalPrincipal,
                                                CnaeFiscalSecundaria,
                                                TipoLogradouro,
                                                Logradouro,
                                                Numero,
                                                Complemento,
                                                Bairro,
                                                CEP,
                                                UF,
                                                Municipio,
                                                DDD1,
                                                Telefone1,
                                                DDD2,
                                                Telefone2,
                                                DDDFax,
                                                Fax,
                                                CorreioEletronico,
                                                SituacaoEspecial,
                                                DataSitucaoEspecial)";

                var _intertValues = @"(@CNPJBase,
                                                @CNPJOrdem,
                                                @CNPJDV,
                                                @IdentificadorMatrizFilial,
                                                @NomeFantasia,
                                                @SituacaoCadastral,
                                                @DataSituacaoCadastral,
                                                @MotivoSituacaoCadastral,
                                                @NomeCidadeExterior,
                                                @Pais,
                                                @DataInicioAtividade,
                                                @CnaeFiscalPrincipal,
                                                @CnaeFiscalSecundaria,
                                                @TipoLogradouro,
                                                @Logradouro,
                                                @Numero,
                                                @Complemento,
                                                @Bairro,
                                                @CEP,
                                                @UF,
                                                @Municipio,
                                                @DDD1,
                                                @Telefone1,
                                                @DDD2,
                                                @Telefone2,
                                                @DDDFax,
                                                @Fax,
                                                @CorreioEletronico,
                                                @SituacaoEspecial,
                                                @DataSitucaoEspecial)";

                var _insert = $"INSERT INTO Estabelecimentos {_insertFields} VALUES {_intertValues}";
                var _data = new Generic();
                var _timer = new Stopwatch();
                _timer.Start();

                foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".ESTABELE"))
                {
                    try
                    {
                        Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

                        using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var fields = line!.Split(';');

                                if (fields[20].ToString().Replace("\"", "") == "6203"
                                    || fields[20].ToString().Replace("\"", "") == "6205"
                                    || fields[20].ToString().Replace("\"", "") == "6219"
                                    || fields[20].ToString().Replace("\"", "") == "6235"
                                    || fields[20].ToString().Replace("\"", "") == "6245"
                                    || fields[20].ToString().Replace("\"", "") == "6259"
                                    || fields[20].ToString().Replace("\"", "") == "6383"
                                    || fields[20].ToString().Replace("\"", "") == "6501"
                                    || fields[20].ToString().Replace("\"", "") == "6541"
                                    || fields[20].ToString().Replace("\"", "") == "6559"
                                    || fields[20].ToString().Replace("\"", "") == "6607"
                                    || fields[20].ToString().Replace("\"", "") == "6697"
                                    || fields[20].ToString().Replace("\"", "") == "6835"
                                    || fields[20].ToString().Replace("\"", "") == "7195")
                                {
                                    _data.ClearParameters();
                                    _data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CNPJOrdem", fields[1].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CNPJDV", fields[2].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@IdentificadorMatrizFilial", fields[3].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@NomeFantasia", fields[4].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@SituacaoCadastral", fields[5].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DataSituacaoCadastral", fields[6].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@MotivoSituacaoCadastral", fields[7].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@NomeCidadeExterior", fields[8].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Pais", fields[9].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DataInicioAtividade", fields[10].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CnaeFiscalPrincipal", fields[11].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CnaeFiscalSecundaria", fields[12].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@TipoLogradouro", fields[13].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Logradouro", fields[14].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Numero", fields[15].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Complemento", fields[16].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Bairro", fields[17].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CEP", fields[18].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@UF", fields[19].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Municipio", fields[20].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DDD1", fields[21].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Telefone1", fields[22].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DDD2", fields[23].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Telefone2", fields[24].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DDDFax", fields[25].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@Fax", fields[26].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@CorreioEletronico", fields[27].ToString().Replace("\"", "").Trim().ToLower());
                                    _data.AddParameters("@SituacaoEspecial", fields[28].ToString().Replace("\"", "").Trim());
                                    _data.AddParameters("@DataSitucaoEspecial", fields[29].ToString().Replace("\"", "").Trim());
                                    await _data.WriteAsync(_insert);
                                    f++;
                                }
                                i++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao conectar: " + ex.Message);
                    }
                }
                _timer.Stop();
                Console.WriteLine($"Registros percorridos {i}, migrados: {f}, {_timer.Elapsed.TotalMinutes} minutes");
            });
    public async Task EmpresasAsync()
        => await Task.Run(async () =>
            {
                int i = 0;
                int e = 0;
                var _insertFields = @"(CNPJBase,RazaoSocial,NaturezaJuridica,QualificacaoResponsavel,CapitalSocial,PorteEmpresa,EnteFederativoResponsavel)";
                var _intertValues = @"(@CNPJBase,@RazaoSocial,@NaturezaJuridica,@QualificacaoResponsavel,@CapitalSocial,@PorteEmpresa,@EnteFederativoResponsavel)";
                var _insert = $"INSERT INTO Empresas {_insertFields} VALUES {_intertValues}";

                var _data = new Generic();
                await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");
                var _timer = new Stopwatch();
                //_timer.Start();

                int parts = 8;
                int size = _data.CNPJBase!.Count() / parts;
                var _list1 = _data.CNPJBase!.Skip(0 * size).Take(size);
                var _list2 = _data.CNPJBase!.Skip(1 * size).Take(size);
                var _list3 = _data.CNPJBase!.Skip(2 * size).Take(size);
                var _list4 = _data.CNPJBase!.Skip(3 * size).Take(size);
                var _list5 = _data.CNPJBase!.Skip(4 * size).Take(size);
                var _list6 = _data.CNPJBase!.Skip(5 * size).Take(size);
                var _list7 = _data.CNPJBase!.Skip(6 * size).Take(size);
                var _list8 = _data.CNPJBase!.Skip(7 * size).Take(size);

                foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".EMPRECSV"))
                {
                    try
                    {
                        Console.WriteLine($"Migrando File {Path.GetFileName(file)}");
                        var _list = new List<Empresa>();
                        using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var fields = line!.Split(';');
                                _list.Add(new Empresa()
                                {
                                    CNPJBase = fields[0].ToString().Replace("\"", ""),
                                    RazaoSocial = fields[1].ToString().Replace("\"", ""),
                                    NaturezaJuridica = fields[2].ToString().Replace("\"", ""),
                                    QualificacaoResponsavel = fields[3].ToString().Replace("\"", ""),
                                    CapitalSocial = fields[4].ToString().Replace("\"", ""),
                                    PorteEmpresa = fields[5].ToString().Replace("\"", ""),
                                    EnteFederativoResponsavel = fields[6].ToString().Replace("\"", "")
                                });
                                i++;
                            }

                        _timer.Start();
                        foreach (var ep in _list)
                        {
                            if (_timer.Elapsed.TotalSeconds <= 10)
                            {

                                var T1 = Task.Run(() =>
                                {
                                    foreach (var item in _list1.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T2 = Task.Run(() =>
                                {
                                    foreach (var item in _list2.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T3 = Task.Run(() =>
                                {
                                    foreach (var item in _list3.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T4 = Task.Run(() =>
                                {
                                    foreach (var item in _list4.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T5 = Task.Run(() =>
                                {
                                    foreach (var item in _list5.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T6 = Task.Run(() =>
                                {
                                    foreach (var item in _list6.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T7 = Task.Run(() =>
                                {
                                    foreach (var item in _list7.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T8 = Task.Run(() =>
                                {
                                    foreach (var item in _list8.Where(s => s == ep.CNPJBase))
                                    {
                                        _data.ClearParameters();
                                        _data.AddParameters("@CNPJBase", ep.CNPJBase!);
                                        _data.AddParameters("@RazaoSocial", ep.RazaoSocial!);
                                        _data.AddParameters("@NaturezaJuridica", ep.NaturezaJuridica!);
                                        _data.AddParameters("@QualificacaoResponsavel", ep.QualificacaoResponsavel!);
                                        _data.AddParameters("@CapitalSocial", ep.CapitalSocial!);
                                        _data.AddParameters("@PorteEmpresa", ep.PorteEmpresa!);
                                        _data.AddParameters("@EnteFederativoResponsavel", ep.EnteFederativoResponsavel!);
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                            }
                        }
                    }
                    catch
                    { Console.WriteLine($"File Not Found"); }
                }

                _timer.Stop();
                Console.WriteLine($"Registros verificados {i}, migrados: {e}, {_timer.Elapsed.TotalMinutes} minutes");
            });
    public async Task SociosAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            int e = 0;
            var _fields = @"(CNPJBase,IdentificadorSocio,NomeRazaoSocio,CnpjCpfSocio,QualificacaoSocio,DataEntradaSociedade,Pais,RepresentanteLegal,NomeRepresentante,QualificacaoRepresentanteLegal,FaixaEtaria)";
            var _values = @"(@CNPJBase,@IdentificadorSocio,@NomeRazaoSocio,@CnpjCpfSocio,@QualificacaoSocio,@DataEntradaSociedade,@Pais,@RepresentanteLegal,@NomeRepresentante,@QualificacaoRepresentanteLegal,@FaixaEtaria)";
            var _insert = $"INSERT INTO Socios {_fields} VALUES {_values}";
            var _data = new Generic();
            await _data.ReadAsync("SELECT CNPJBase FROM Empresas");
            var _timer = new Stopwatch();
            _timer.Start();


            int parts = 8;
            int size = _data.CNPJBase!.Count() / parts;
            var _list1 = _data.CNPJBase!.Skip(0 * size).Take(size);
            var _list2 = _data.CNPJBase!.Skip(1 * size).Take(size);
            var _list3 = _data.CNPJBase!.Skip(2 * size).Take(size);
            var _list4 = _data.CNPJBase!.Skip(3 * size).Take(size);
            var _list5 = _data.CNPJBase!.Skip(4 * size).Take(size);
            var _list6 = _data.CNPJBase!.Skip(5 * size).Take(size);
            var _list7 = _data.CNPJBase!.Skip(6 * size).Take(size);
            var _list8 = _data.CNPJBase!.Skip(7 * size).Take(size);

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".SOCIOCSV"))
            {
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");
                    var _list = new List<Socio>();
                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');

                            _list.Add(new Socio()
                            {
                                CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
                                IdentificadorSocio = fields[1].ToString().Replace("\"", "").Trim(),
                                NomeRazaoSocio = fields[2].ToString().Replace("\"", "").Trim(),
                                CnpjCpfSocio = fields[3].ToString().Replace("\"", "").Trim(),
                                QualificacaoSocio = fields[4].ToString().Replace("\"", "").Trim(),
                                DataEntradaSociedade = fields[5].ToString().Replace("\"", "").Trim(),
                                Pais = fields[6].ToString().Replace("\"", "").Trim(),
                                RepresentanteLegal = fields[7].ToString().Replace("\"", "").Trim(),
                                NomeRepresentante = fields[8].ToString().Replace("\"", "").Trim(),
                                QualificacaoRepresentanteLegal = fields[9].ToString().Replace("\"", "").Trim(),
                                FaixaEtaria = fields[10].ToString().Replace("\"", "").Trim()
                            });
                        }


                    foreach (var sc in _list)
                    {

                        var T1 = Task.Run(async () =>
                        {
                            foreach (var item in _list1.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T2 = Task.Run(async () =>
                        {
                            foreach (var item in _list2.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T3 = Task.Run(async () =>
                        {
                            foreach (var item in _list3.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T4 = Task.Run(async () =>
                        {
                            foreach (var item in _list4.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T5 = Task.Run(async () =>
                        {
                            foreach (var item in _list5.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T6 = Task.Run(async () =>
                        {
                            foreach (var item in _list6.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T7 = Task.Run(async () =>
                        {
                            foreach (var item in _list7.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });

                        var T8 = Task.Run(async () =>
                        {
                            foreach (var item in _list8.Where(s => s == sc.CNPJBase))
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", sc.CNPJBase!);
                                _data.AddParameters("@IdentificadorSocio", sc.IdentificadorSocio!);
                                _data.AddParameters("@NomeRazaoSocio", sc.NomeRazaoSocio!);
                                _data.AddParameters("@CnpjCpfSocio", sc.CnpjCpfSocio!);
                                _data.AddParameters("@QualificacaoSocio", sc.QualificacaoSocio!);
                                _data.AddParameters("@DataEntradaSociedade", sc.DataEntradaSociedade!);
                                _data.AddParameters("@Pais", sc.Pais!);
                                _data.AddParameters("@RepresentanteLegal", sc.RepresentanteLegal!);
                                _data.AddParameters("@NomeRepresentante", sc.NomeRepresentante!);
                                _data.AddParameters("@QualificacaoRepresentanteLegal", sc.QualificacaoRepresentanteLegal!);
                                _data.AddParameters("@FaixaEtaria", sc.FaixaEtaria!);
                                await _data.WriteAsync(_insert);
                                e++;
                                Console.WriteLine($"Item {e} migrado!");
                            }
                        });
                        await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
            }

            _timer.Stop();
            Console.WriteLine($"Registros percorridos {i}, migrados: {e}, {_timer.Elapsed.TotalMinutes} minutes");

        });
    public async Task SimplesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            int e = 0;
            var _fields = @"(CNPJBase,OpcaoSimples,DataOpcaoSimples,DataExclusaoSimples,OpcaoMEI,DataOpcaoMEI,DataExclusaoMEI)";
            var _values = @"(@CNPJBase,@OpcaoSimples,@DataOpcaoSimples,@DataExclusaoSimples,@OpcaoMEI,@DataOpcaoMEI,@DataExclusaoMEI)";
            var _insert = $"INSERT INTO Simples {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();
            var _data = new Generic();
            await _data.ReadAsync("SELECT CNPJBase FROM Empresas");

            var _list = new List<Simples>();

            int parts = 8;
            int size = _data.CNPJBase!.Count() / parts;
            var _list1 = _data.CNPJBase!.Skip(0 * size).Take(size);
            var _list2 = _data.CNPJBase!.Skip(1 * size).Take(size);
            var _list3 = _data.CNPJBase!.Skip(2 * size).Take(size);
            var _list4 = _data.CNPJBase!.Skip(3 * size).Take(size);
            var _list5 = _data.CNPJBase!.Skip(4 * size).Take(size);
            var _list6 = _data.CNPJBase!.Skip(5 * size).Take(size);
            var _list7 = _data.CNPJBase!.Skip(6 * size).Take(size);
            var _list8 = _data.CNPJBase!.Skip(7 * size).Take(size);

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".D30311"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');

                            _list.Add(new Simples()
                            {
                                CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
                                OpcaoSimples = fields[1].ToString().Replace("\"", "").Trim(),
                                DataOpcaoSimples = fields[2].ToString().Replace("\"", "").Trim(),
                                DataExclusaoSimples = fields[3].ToString().Replace("\"", "").Trim(),
                                OpcaoMEI = fields[4].ToString().Replace("\"", "").Trim(),
                                DataOpcaoMEI = fields[5].ToString().Replace("\"", "").Trim(),
                                DataExclusaoMEI = fields[6].ToString().Replace("\"", "").Trim()
                            });
                        }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }

            foreach (var sn in _list)
            {

                var T1 = Task.Run(async () =>
                {
                    foreach (var item in _list1.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T2 = Task.Run(async () =>
                {
                    foreach (var item in _list2.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T3 = Task.Run(async () =>
                {
                    foreach (var item in _list3.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T4 = Task.Run(async () =>
                {
                    foreach (var item in _list4.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T5 = Task.Run(async () =>
                {
                    foreach (var item in _list5.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T6 = Task.Run(async () =>
                {
                    foreach (var item in _list6.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T7 = Task.Run(async () =>
                {
                    foreach (var item in _list7.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });

                var T8 = Task.Run(async () =>
                {
                    foreach (var item in _list8.Where(s => s == sn.CNPJBase))
                    {
                        _data.ClearParameters();
                        _data.AddParameters("@CNPJBase", sn.CNPJBase!);
                        _data.AddParameters("@OpcaoSimples", sn.OpcaoSimples!);
                        _data.AddParameters("@DataOpcaoSimples", sn.DataOpcaoSimples!);
                        _data.AddParameters("@DataExclusaoSimples", sn.DataExclusaoSimples!);
                        _data.AddParameters("@OpcaoMEI", sn.OpcaoMEI!);
                        _data.AddParameters("@DataOpcaoMEI", sn.DataOpcaoMEI!);
                        _data.AddParameters("@DataExclusaoMEI", sn.DataExclusaoMEI!);
                        await _data.WriteAsync(_insert);
                        e++;
                        Console.WriteLine($"Item {e} migrado!");
                    }
                });
                await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                i++;
            }

            _timer.Stop();
            Console.WriteLine($"Registros percorridos {i}, migrados: {e}, {_timer.Elapsed.TotalMinutes} minutes");
        });


    public async Task CnaesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Cnaes {_fields} VALUES {_values}";
            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".CNAECSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });

    public async Task MotivoAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO MotivoSituacaoCadastral {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".MOTICSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });

    public async Task MunicipioAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Municipios {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".MUNICCSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });

    public async Task NaturezaAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO NaturezaJuridica {_fields} VALUES {_values}";
            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".NATJUCSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });

    public async Task PaisesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Paises {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".PAISCSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });

    public async Task QualificaAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO QualificacaoSocios {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();

            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".QUALSCSV"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");

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
                    Console.WriteLine($"Registros percorridos {i}, migrados: {i}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
        });


}