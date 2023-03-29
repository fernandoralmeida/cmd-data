namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {        
        Console.WriteLine(Environment.GetEnvironmentVariable("connection_string_migradata"));
        while (true)
        {
            Console.WriteLine("Iniciando Sistema!");
            Thread.Sleep(3000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Escolha a opção desejada!");
            Console.WriteLine("----------");
            Console.WriteLine("1. Migrate");
            Console.WriteLine("----------");
            Console.WriteLine("5. Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Normalize Files");
                    await new ListFiles().NormalizeFile(@"C:\data");
                    await new Migrate().Normalize();
                    Console.WriteLine("Iniciando migração...");
                    await new Migrate().EstabelecimentosAsyn();
                    await new Migrate().EmpresasAsync();
                    break;
                case 5:
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
