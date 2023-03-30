using System.Diagnostics;
using System.Text;
using migradata.Models;

namespace migradata;

public class Migrate3
{
    public async Task Normalize()
    {
        var data = new Data();
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
    public async Task EmpresasAsync()
    => await Task.Run(async () =>
        {
            int i = 0;
            //int e = 0;
            int c1 = 0;
            int c2 = 0;
            int c3 = 0;
            int c4 = 0;
            int c5 = 0;
            int c6 = 0;
            int c7 = 0;
            int c8 = 0;
            var _insertFields = @"(CNPJBase,RazaoSocial,NaturezaJuridica,QualificacaoResponsavel,CapitalSocial,PorteEmpresa,EnteFederativoResponsavel)";
            var _intertValues = @"(@CNPJBase,@RazaoSocial,@NaturezaJuridica,@QualificacaoResponsavel,@CapitalSocial,@PorteEmpresa,@EnteFederativoResponsavel)";
            var _insert = $"INSERT INTO Empresas {_insertFields} VALUES {_intertValues}";

            var _data = new Data();
            await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");
            var _timer = new Stopwatch();

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

                    int parts = 8;
                    int size = _list.Count() / parts;
                    var _list1 = _list.Skip(0 * size).Take(size);
                    var _list2 = _list.Skip(1 * size).Take(size);
                    var _list3 = _list.Skip(2 * size).Take(size);
                    var _list4 = _list.Skip(3 * size).Take(size);
                    var _list5 = _list.Skip(4 * size).Take(size);
                    var _list6 = _list.Skip(5 * size).Take(size);
                    var _list7 = _list.Skip(6 * size).Take(size);
                    var _list8 = _list.Skip(7 * size).Take(size);

                    _timer.Start();

                    var T1 = Task.Run(async () =>
                    {
                        foreach (var item in _list1)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c1++;
                            }
                        }
                    });

                    var T2 = Task.Run(async () =>
                    {
                        foreach (var item in _list2)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c2++;
                            }
                        }
                    });

                    var T3 = Task.Run(async () =>
                    {
                        foreach (var item in _list3)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c3++;
                            }
                        }
                    });

                    var T4 = Task.Run(async () =>
                    {
                        foreach (var item in _list4)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c4++;
                            }
                        }
                    });

                    var T5 = Task.Run(async () =>
                    {
                        foreach (var item in _list5)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c5++;
                            }
                        }
                    });

                    var T6 = Task.Run(async () =>
                    {
                        foreach (var item in _list6)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c6++;
                            }
                        }
                    });

                    var T7 = Task.Run(async () =>
                    {
                        foreach (var item in _list7)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c7++;
                            }
                        }
                    });

                    var T8 = Task.Run(async () =>
                    {
                        foreach (var item in _list8)
                        {
                            foreach (var ep in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@RazaoSocial", item.RazaoSocial!);
                                _migra.AddParameters("@NaturezaJuridica", item.NaturezaJuridica!);
                                _migra.AddParameters("@QualificacaoResponsavel", item.QualificacaoResponsavel!);
                                _migra.AddParameters("@CapitalSocial", item.CapitalSocial!);
                                _migra.AddParameters("@PorteEmpresa", item.PorteEmpresa!);
                                _migra.AddParameters("@EnteFederativoResponsavel", item.EnteFederativoResponsavel!);
                                await _migra.WriteAsync(_insert);
                                c8++;
                            }
                        }
                    });

                    await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);


                }
                catch
                { Console.WriteLine($"File Not Found"); }
            }

            _timer.Stop();
            Console.WriteLine($"Registros verificados {i}, migrados: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
        });

    public async Task SociosAsync()
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
            var _fields = @"(CNPJBase,IdentificadorSocio,NomeRazaoSocio,CnpjCpfSocio,QualificacaoSocio,DataEntradaSociedade,Pais,RepresentanteLegal,NomeRepresentante,QualificacaoRepresentanteLegal,FaixaEtaria)";
            var _values = @"(@CNPJBase,@IdentificadorSocio,@NomeRazaoSocio,@CnpjCpfSocio,@QualificacaoSocio,@DataEntradaSociedade,@Pais,@RepresentanteLegal,@NomeRepresentante,@QualificacaoRepresentanteLegal,@FaixaEtaria)";
            var _insert = $"INSERT INTO Socios {_fields} VALUES {_values}";
            var _data = new Data();
            await _data.ReadAsync("SELECT CNPJBase FROM Empresas");
            var _timer = new Stopwatch();
            _timer.Start();

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


                    int parts = 8;
                    int size = _list.Count() / parts;
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
                        foreach (var item in _list1)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c1++;
                            }
                        }
                    });

                    var T2 = Task.Run(async () =>
                    {
                        foreach (var item in _list2)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c2++;
                            }
                        }
                    });

                    var T3 = Task.Run(async () =>
                    {
                        foreach (var item in _list3)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c3++;
                            }
                        }
                    });

                    var T4 = Task.Run(async () =>
                    {
                        foreach (var item in _list4)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c4++;
                            }
                        }
                    });

                    var T5 = Task.Run(async () =>
                    {
                        foreach (var item in _list5)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c5++;
                            }
                        }
                    });

                    var T6 = Task.Run(async () =>
                    {
                        foreach (var item in _list6)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c6++;
                            }
                        }
                    });

                    var T7 = Task.Run(async () =>
                    {
                        foreach (var item in _list7)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c7++;
                            }
                        }
                    });

                    var T8 = Task.Run(async () =>
                    {
                        foreach (var item in _list8)
                        {
                            foreach (var sc in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@IdentificadorSocio", item.IdentificadorSocio!);
                                _migra.AddParameters("@NomeRazaoSocio", item.NomeRazaoSocio!);
                                _migra.AddParameters("@CnpjCpfSocio", item.CnpjCpfSocio!);
                                _migra.AddParameters("@QualificacaoSocio", item.QualificacaoSocio!);
                                _migra.AddParameters("@DataEntradaSociedade", item.DataEntradaSociedade!);
                                _migra.AddParameters("@Pais", item.Pais!);
                                _migra.AddParameters("@RepresentanteLegal", item.RepresentanteLegal!);
                                _migra.AddParameters("@NomeRepresentante", item.NomeRepresentante!);
                                _migra.AddParameters("@QualificacaoRepresentanteLegal", item.QualificacaoRepresentanteLegal!);
                                _migra.AddParameters("@FaixaEtaria", item.FaixaEtaria!);
                                await _migra.WriteAsync(_insert);
                                c8++;
                            }
                        }
                    });
                    await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                    i++;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
            }

            _timer.Stop();
            Console.WriteLine($"Registros percorridos {i}, migrados: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");

        });
    public async Task SimplesAsync()
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
            var _fields = @"(CNPJBase,OpcaoSimples,DataOpcaoSimples,DataExclusaoSimples,OpcaoMEI,DataOpcaoMEI,DataExclusaoMEI)";
            var _values = @"(@CNPJBase,@OpcaoSimples,@DataOpcaoSimples,@DataExclusaoSimples,@OpcaoMEI,@DataOpcaoMEI,@DataExclusaoMEI)";
            var _insert = $"INSERT INTO Simples {_fields} VALUES {_values}";

            var _timer = new Stopwatch();
            _timer.Start();
            var _data = new Data();
            await _data.ReadAsync("SELECT CNPJBase FROM Empresas");



            foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".D30311"))
                try
                {
                    Console.WriteLine($"Migrando File {Path.GetFileName(file)}");
                    var _list = new List<Simples>();

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
                            i++;
                        }

                    int parts = 8;

                    int size = _list.Count() / parts;
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
                           foreach (var item in _list1)
                           {
                               foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                               {
                                   var _migra = new Data();
                                   _migra.ClearParameters();
                                   _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                   _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                   _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                   _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                   _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                   _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                   _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                   await _migra.WriteAsync(_insert);
                                   c1++;
                               }
                           }
                       });

                    var T2 = Task.Run(async () =>
                    {
                        foreach (var item in _list2)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c2++;
                            }
                        }
                    });

                    var T3 = Task.Run(async () =>
                    {
                        foreach (var item in _list3)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c3++;
                            }
                        }
                    });

                    var T4 = Task.Run(async () =>
                    {
                        foreach (var item in _list4)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c4++;
                            }
                        }
                    });

                    var T5 = Task.Run(async () =>
                    {
                        foreach (var item in _list5)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c5++;
                            }
                        }
                    });

                    var T6 = Task.Run(async () =>
                    {
                        foreach (var item in _list6)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c6++;
                            }
                        }
                    });

                    var T7 = Task.Run(async () =>
                    {
                        foreach (var item in _list7)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c7++;
                            }
                        }
                    });

                    var T8 = Task.Run(async () =>
                    {
                        foreach (var item in _list8)
                        {
                            foreach (var sn in _data.CNPJBase!.Where(s => s == item.CNPJBase))
                            {
                                var _migra = new Data();
                                _migra.ClearParameters();
                                _migra.AddParameters("@CNPJBase", item.CNPJBase!);
                                _migra.AddParameters("@OpcaoSimples", item.OpcaoSimples!);
                                _migra.AddParameters("@DataOpcaoSimples", item.DataOpcaoSimples!);
                                _migra.AddParameters("@DataExclusaoSimples", item.DataExclusaoSimples!);
                                _migra.AddParameters("@OpcaoMEI", item.OpcaoMEI!);
                                _migra.AddParameters("@DataOpcaoMEI", item.DataOpcaoMEI!);
                                _migra.AddParameters("@DataExclusaoMEI", item.DataExclusaoMEI!);
                                await _migra.WriteAsync(_insert);
                                c8++;
                            }
                        }
                    });

                    await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);
                    i++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }

            _timer.Stop();
            Console.WriteLine($"Registros percorridos {i}, migrados: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
        });


}