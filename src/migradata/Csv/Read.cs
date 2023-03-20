using System.Diagnostics;
using System.Text;

namespace migradata.Csv;

public class Read
{
    public async Task MigraEmpresasAsync()
        => await Task.Run(async () =>
            {
                int i = 0;
                int e = 0;
                var _insertFields = @"(CNPJBase,RazaoSocial,NaturezaJuridica,QualificacaoResponsavel,CapitalSocial,PorteEmpresa,EnteFederativoResponsavel)";
                var _intertValues = @"(@CNPJBase,@RazaoSocial,@NaturezaJuridica,@QualificacaoResponsavel,@CapitalSocial,@PorteEmpresa,@EnteFederativoResponsavel)";
                var _insert = $"INSERT INTO Empresas {_insertFields} VALUES {_intertValues}";

                var _data = new Data();
                await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");

                for (int x = 0; x < 10; x++)
                {
                    try
                    {
                        Console.WriteLine($"File K3241.K03200Y{x}.D30211.EMPRECSV");
                        var _timer = new Stopwatch();
                        _timer.Start();

                        using (var reader = new StreamReader($"c:/data/K3241.K03200Y{x}.D30211.EMPRECSV", Encoding.GetEncoding("ISO-8859-1")))
                            while (!reader.EndOfStream)
                            {                                
                                var line = reader.ReadLine();
                                var fields = line!.Split(';');                                

                                if (_data.CNPJBase!.Where(s => s == fields[0].ToString().Replace("\"", "")).Count() > 0)
                                {
                                    _data.ClearParameters();
                                    _data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", ""));
                                    _data.AddParameters("@RazaoSocial", fields[1].ToString().Replace("\"", ""));
                                    _data.AddParameters("@NaturezaJuridica", fields[2].ToString().Replace("\"", ""));
                                    _data.AddParameters("@QualificacaoResponsavel", fields[3].ToString().Replace("\"", ""));
                                    _data.AddParameters("@CapitalSocial", fields[4].ToString().Replace("\"", ""));
                                    _data.AddParameters("@PorteEmpresa", fields[5].ToString().Replace("\"", ""));
                                    _data.AddParameters("@EnteFederativoResponsavel", fields[6].ToString().Replace("\"", ""));
                                    await _data.WriteAsync(_insert);
                                    e++;
                                }
                                i++;
                            }
                        _timer.Stop();
                        Console.WriteLine($"Registros verificados {i}, migrados: {e}, {_timer.Elapsed.TotalMinutes} minutes");
                    }
                    catch
                    { }
                }

            });

    public async Task MigrateEstabelecimentosAsyn()
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

                for (int x = 0; x < 10; x++)
                {
                    try
                    {
                        Console.WriteLine($"File K3241.K03200Y{x}.D30211.ESTABELE");
                        var _timer = new Stopwatch();
                        _timer.Start();
                        
                        using (var reader = new StreamReader($"c:/data/K3241.K03200Y{x}.D30211.ESTABELE", Encoding.GetEncoding("ISO-8859-1")))
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
                        _timer.Stop();
                        Console.WriteLine($"Registros percorridos {i}, migrados: {f}, {_timer.Elapsed.TotalMinutes} minutes");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao conectar: " + ex.Message);
                    }
                }
            });

    public async Task MigraSociosAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            int s = 0;
            var _fields = @"(CNPJBase,IdentificadorSocio,NomeRazaoSocio,CnpjCpfSocio,QualificacaoSocio,DataEntradaSociedade,Pais,RepresentanteLegal,NomeRepresentante,QualificacaoRepresentanteLegal,FaixaEtaria)";
            var _values = @"(@CNPJBase,@IdentificadorSocio,@NomeRazaoSocio,@CnpjCpfSocio,@QualificacaoSocio,@DataEntradaSociedade,@Pais,@RepresentanteLegal,@NomeRepresentante,@QualificacaoRepresentanteLegal,@FaixaEtaria)";
            var _insert = $"INSERT INTO Socios {_fields} VALUES {_values}";
            var _data = new Data();
            await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");

            for (int x = 0; x < 10; x++)
                try
                {
                    Console.WriteLine($"File K3241.K03200Y{x}.D30211.SOCIOCSV");
                    var _timer = new Stopwatch();
                    _timer.Start();

                    using (var reader = new StreamReader($"c:/data/K3241.K03200Y{x}.D30211.SOCIOCSV", Encoding.GetEncoding("ISO-8859-1")))
                        while (!reader.EndOfStream)
                        {                            
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            if (_data.CNPJBase!.Where(s => s == fields[0].ToString().Replace("\"", "")).Count() > 0)
                            {
                                _data.ClearParameters();
                                _data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@IdentificadorSocio", fields[1].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@NomeRazaoSocio", fields[2].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@CnpjCpfSocio", fields[3].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@QualificacaoSocio", fields[4].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@DataEntradaSociedade", fields[5].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@Pais", fields[6].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@RepresentanteLegal", fields[7].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@NomeRepresentante", fields[8].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@QualificacaoRepresentanteLegal", fields[9].ToString().Replace("\"", "").Trim());
                                _data.AddParameters("@FaixaEtaria", fields[10].ToString().Replace("\"", "").Trim());
                                await _data.WriteAsync(_insert);
                                s++;
                            }
                            i++;
                        }

                    _timer.Stop();
                    Console.WriteLine($"Registros percorridos {i}, migrados: {s}, {_timer.Elapsed.TotalMinutes} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }

        });

    public async Task MigraSimplesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            int s = 0;
            var _fields = @"(CNPJBase,OpcaoSimples,DataOpcaoSimples,DataExclusaoSimples,OpcaoMEI,DataOpcaoMEI,DataExclusaoMEI)";
            var _values = @"(@CNPJBase,@OpcaoSimples,@DataOpcaoSimples,@DataExclusaoSimples,@OpcaoMEI,@DataOpcaoMEI,@DataExclusaoMEI)";
            var _insert = $"INSERT INTO Simples {_fields} VALUES {_values}";
            
            try
            {
                Console.WriteLine($"File F.K03200$W.SIMPLES.CSV.D30211");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();
                await _data.ReadAsync("SELECT CNPJBase FROM Estabelecimentos");

                using (var reader = new StreamReader($"c:/data/F.K03200$W.SIMPLES.CSV.D30211", Encoding.GetEncoding("ISO-8859-1")))
                    while (!reader.EndOfStream)
                    {                        
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        if (_data.CNPJBase!.Where(s => s == fields[0].ToString().Replace("\"", "")).Count() > 0)
                        {
                            _data.ClearParameters();
                            _data.AddParameters("@CNPJBase", fields[0].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@OpcaoSimples", fields[1].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@DataOpcaoSimples", fields[2].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@DataExclusaoSimples", fields[3].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@OpcaoMEI", fields[4].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@DataOpcaoMEI", fields[5].ToString().Replace("\"", "").Trim());
                            _data.AddParameters("@DataExclusaoMEI", fields[6].ToString().Replace("\"", "").Trim());
                            await _data.WriteAsync(_insert);
                            s++;
                        }
                        i++;
                    }

                _timer.Stop();
                Console.WriteLine($"Registros percorridos {i}, migrados: {s}, {_timer.Elapsed.TotalMinutes} minutes");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
            }

        });

    public async Task MigraCnaesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Cnaes {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.CNAECSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();

                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.CNAECSV", Encoding.GetEncoding("ISO-8859-1")))
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

    public async Task MigraMotivoAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO MotivoSituacaoCadastral {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.MOTICSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();
                
                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.MOTICSV", Encoding.GetEncoding("ISO-8859-1")))
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

    public async Task MigraMunicipioAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Municipios {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.MUNICCSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();

                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.MUNICCSV", Encoding.GetEncoding("ISO-8859-1")))
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

    public async Task MigraNaturezaAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO NaturezaJuridica {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.NATJUCSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();

                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.NATJUCSV", Encoding.GetEncoding("ISO-8859-1")))
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

    public async Task MigraPaiesesAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO Paises {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.PAISCSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();

                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.PAISCSV", Encoding.GetEncoding("ISO-8859-1")))
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

    public async Task MigraQualificaAsync()
        => await Task.Run(async () =>
        {
            int i = 0;
            var _fields = @"(Codigo,Descricao)";
            var _values = @"(@Codigo,@Descricao)";
            var _insert = $"INSERT INTO QualificacaoSocios {_fields} VALUES {_values}";
            try
            {
                Console.WriteLine($"File F.K03200$Z.D30311.QUALSCSV");
                var _timer = new Stopwatch();
                _timer.Start();
                var _data = new Data();
                using (var reader = new StreamReader($"c:/data/F.K03200$Z.D30311.QUALSCSV", Encoding.GetEncoding("ISO-8859-1")))
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