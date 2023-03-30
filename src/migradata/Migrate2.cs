using System.Diagnostics;
using System.Text;
using migradata.Models;

namespace migradata;

public class Migrate2
{
    public async Task EmpresasAsync()
    => await Task.Run(async () =>
        {
            int i = 0;
            int e = 0;
            var _insertFields = @"(CNPJBase,RazaoSocial,NaturezaJuridica,QualificacaoResponsavel,CapitalSocial,PorteEmpresa,EnteFederativoResponsavel)";
            var _intertValues = @"(@CNPJBase,@RazaoSocial,@NaturezaJuridica,@QualificacaoResponsavel,@CapitalSocial,@PorteEmpresa,@EnteFederativoResponsavel)";
            var _insert = $"INSERT INTO Empresas {_insertFields} VALUES {_intertValues}";

            var _data = new Data();
            await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");
            var _timer = new Stopwatch();
            
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
                    _timer.Start();
                    using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');

                            var _cnpjbase = fields[0].ToString().Replace("\"", "");

                            _data.ClearParameters();
                            _data.AddParameters("@CNPJBase", _cnpjbase);
                            _data.AddParameters("@RazaoSocial", fields[1].ToString().Replace("\"", ""));
                            _data.AddParameters("@NaturezaJuridica", fields[2].ToString().Replace("\"", ""));
                            _data.AddParameters("@QualificacaoResponsavel", fields[3].ToString().Replace("\"", ""));
                            _data.AddParameters("@CapitalSocial", fields[4].ToString().Replace("\"", ""));
                            _data.AddParameters("@PorteEmpresa", fields[5].ToString().Replace("\"", ""));
                            _data.AddParameters("@EnteFederativoResponsavel", fields[6].ToString().Replace("\"", ""));
                            i++;

                            if (_timer.Elapsed.TotalSeconds <= 10)
                            {

                                var T1 = Task.Run(() =>
                                {
                                    foreach (var item in _list1.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T2 = Task.Run(() =>
                                {
                                    foreach (var item in _list2.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T3 = Task.Run(() =>
                                {
                                    foreach (var item in _list3.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T4 = Task.Run(() =>
                                {
                                    foreach (var item in _list4.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T5 = Task.Run(() =>
                                {
                                    foreach (var item in _list5.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T6 = Task.Run(() =>
                                {
                                    foreach (var item in _list6.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T7 = Task.Run(() =>
                                {
                                    foreach (var item in _list7.Where(s => s == _cnpjbase))
                                    {
                                        //await _data.WriteAsync(_insert);
                                        e++;
                                        Console.WriteLine($"Item {e} migrado!");
                                    }
                                });

                                var T8 = Task.Run(() =>
                                {
                                    foreach (var item in _list8.Where(s => s == _cnpjbase))
                                    {
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

}