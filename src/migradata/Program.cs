using migradata.Helpers;

namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("connection_string_migradata"));
        while (true)
        {
            Console.WriteLine("Start System!");
            Thread.Sleep(3000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Choose Options!");
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
                    await new NormalizeFiles().Start(@"C:\data");
                    await Migrate.MgNormalize.StartAsync();
                    Console.WriteLine("Start migration...");
                   
                    await Task.WhenAll(
                        Migrate.MgCnaes.StartAsync(),
                        Migrate.MgMotivos.StartAsync(),
                        Migrate.MgMunicipios.StartAsync(),
                        Migrate.MgNatureza.StartAsync(),
                        Migrate.MgPaises.StartAsync(),
                        Migrate.MgQualifica.StartAsync());                    
                    
                    await Migrate.MgEstabelecimentos.StartAsync();
                    await Migrate.MgEmpresas.StartAsync();
                    await Migrate.MgSocios.StartAsync();
                    await Migrate.MgSimples.StartAsync();
                    break;
                case 5:
                    Console.WriteLine("Closing App...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again!");
                    break;
            }

            Console.WriteLine();
        }
    }
}
