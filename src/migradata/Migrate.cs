using System.Diagnostics;
using System.Text;

namespace migradata;

public class Migrate
{
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
                var _data = new Data();
                var _timer = new Stopwatch();
                _timer.Start();
                

                foreach(var file in await new ListFiles().DoListAync(@"C:\data", ".ESTABELE"))
                {
                    try
                    {
                        Console.WriteLine($"File {Path.GetFileName(file)}");

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
                                    //await _data.WriteAsync(_insert);
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

                var _data = new Data();
                //await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");
                var _timer = new Stopwatch();
                _timer.Start();

                foreach (var file in await new ListFiles().DoListAync(@"C:\data", ".EMPRECSV"))
                {
                    try
                    {
                        Console.WriteLine($"File {Path.GetFileName(file)}");

                        using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var fields = line!.Split(';');
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", ""));
                                _data.AddParameters("@RazaoSocial", fields[1].ToString().Replace("\"", ""));
                                _data.AddParameters("@NaturezaJuridica", fields[2].ToString().Replace("\"", ""));
                                _data.AddParameters("@QualificacaoResponsavel", fields[3].ToString().Replace("\"", ""));
                                _data.AddParameters("@CapitalSocial", fields[4].ToString().Replace("\"", ""));
                                _data.AddParameters("@PorteEmpresa", fields[5].ToString().Replace("\"", ""));
                                _data.AddParameters("@EnteFederativoResponsavel", fields[6].ToString().Replace("\"", ""));
                                //await _data.WriteAsync(_insert);
                                i++;
                            }
                    }
                    catch
                    { }
                }
                _timer.Start();
                //await _data.WriteAsync(@"DELETE FROM Empresas WHERE NOT EXISTS (SELECT 1 FROM Estabelecimentos WHERE Estabelecimentos.CNPJBase = Empresas.CNPJBase)");
                //await _data.ReadAsync(@"SELECT CNPJBase FROM Empresas");
                //e = _data.CNPJBase!.Count();
                _timer.Stop();
                Console.WriteLine($"Registros verificados {i}, migrados: {e}, {_timer.Elapsed.TotalMinutes} minutes");
            });

}