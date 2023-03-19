namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("Iniciando Sistema!");
        Console.WriteLine(Environment.GetEnvironmentVariable("connection_string_migradata"));        
        while(true)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("--- Migra-Data ---");
            Console.WriteLine("------------------");
            Console.WriteLine("1. Start Migration");
            Console.WriteLine("2. Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    var _data = new Csv.Read();
                    Console.WriteLine("Preparando dados...");
                    await new Data().WriteAsync(@"DELETE FROM Cnaes");
                    await new Data().WriteAsync(@"DELETE FROM MotivoSituacaoCadastral");
                    await new Data().WriteAsync(@"DELETE FROM Municipios");
                    await new Data().WriteAsync(@"DELETE FROM NaturezaJuridica");
                    await new Data().WriteAsync(@"DELETE FROM Paises");
                    await new Data().WriteAsync(@"DELETE FROM QualificacaoSocios");
                    await new Data().WriteAsync(@"DELETE FROM Estabelecimentos");
                    await new Data().WriteAsync(@"DELETE FROM Empresas");
                    await new Data().WriteAsync(@"DELETE FROM Socios");
                    await new Data().WriteAsync(@"DELETE FROM Simples");
                    Console.WriteLine("Iniciando migração...");
                    await _data.MigraCnaesAsync();
                    await _data.MigraMotivoAsync();
                    await _data.MigraMunicipioAsync();
                    await _data.MigraQualificaAsync();
                    await _data.MigraNaturezaAsync();
                    await _data.MigraPaiesesAsync();
                    await _data.MigrateEstabelecimentosAsyn();
                    await _data.MigraEmpresasAsync();
                    await _data.MigraSociosAsync();
                    await _data.MigraSimplesAsync();
                    break;
                case 2:
                    Console.WriteLine("Encerrando a aplicação...");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
