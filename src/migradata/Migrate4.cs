using System.Diagnostics;
using System.Text;
using migradata.Models;
using migradata.Repositories;
using migradata.Helpers;

namespace migradata;

public class Migrate4
{
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

            var _data = new Generic();
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
                    int size = (_list.Count() / parts)+1;
                    int _end = (size * 8) - _list.Count();
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
                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c1}");

                        }
                    });

                    var T2 = Task.Run(async () =>
                    {
                        foreach (var item in _list2)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c2}");
                        }
                    });

                    var T3 = Task.Run(async () =>
                    {
                        foreach (var item in _list3)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c3}");

                        }
                    });

                    var T4 = Task.Run(async () =>
                    {
                        foreach (var item in _list4)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c4}");

                        }
                    });

                    var T5 = Task.Run(async () =>
                    {
                        foreach (var item in _list5)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c5}");

                        }
                    });

                    var T6 = Task.Run(async () =>
                    {
                        foreach (var item in _list6)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c6}");

                        }
                    });

                    var T7 = Task.Run(async () =>
                    {
                        foreach (var item in _list7)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c7}");

                        }
                    });

                    var T8 = Task.Run(async () =>
                    {
                        foreach (var item in _list8)
                        {

                            var _migra = new Generic();
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
                            //Console.WriteLine($"{c8}");
                        }
                    });

                    await Task.WhenAll(T1, T2, T3, T4, T5, T6, T7, T8);                    

                }
                catch (Exception ex)
                { Console.WriteLine($"File Not Found: Erro! {ex.Message}"); }
            }
            _timer.Stop();
            Console.WriteLine($"Registros verificados {i}, migrados: {c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8}, {_timer.Elapsed.TotalMinutes} minutes");
        });
}