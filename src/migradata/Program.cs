using migradata.Helpers;

namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Start System!");
            Thread.Sleep(3000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Choose Options!");
            Console.WriteLine("----------");
            Console.WriteLine("1. Migrate To SqlServer");
            Console.WriteLine("2. Migrate To MySql");
            Console.WriteLine("3. Create In SqlServer");
            Console.WriteLine("4. Create In MySql");
            Console.WriteLine("----------");
            Console.WriteLine("5. Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Normalize Files");
                    await new NormalizeFiles().Start(@"C:\data");
                    await Migrate.MgNormalize.StartAsync(TServer.SqlServer);
                    Console.WriteLine("Start migration...");
                   
                    await Task.WhenAll(
                        Migrate.MgCnaes.StartAsync(TServer.SqlServer),
                        Migrate.MgMotivos.StartAsync(TServer.SqlServer),
                        Migrate.MgMunicipios.StartAsync(TServer.SqlServer),
                        Migrate.MgNatureza.StartAsync(TServer.SqlServer),
                        Migrate.MgPaises.StartAsync(TServer.SqlServer),
                        Migrate.MgQualifica.StartAsync(TServer.SqlServer));                    
                    
                    await Migrate.MgEstabelecimentos.StartAsync(TServer.SqlServer);
                    await Migrate.MgEmpresas.StartAsync(TServer.SqlServer);
                    await Migrate.MgSocios.StartAsync(TServer.SqlServer);
                    await Migrate.MgSimples.StartAsync(TServer.SqlServer);
                    break;
                case 2:
                    Console.WriteLine("Normalize Files");
                    await new NormalizeFiles().Start(@"C:\data");
                    await Migrate.MgNormalize.StartAsync(TServer.MySql);
                    Console.WriteLine("Start migration...");

                    await Task.WhenAll(
                        Migrate.MgCnaes.StartAsync(TServer.MySql),
                        Migrate.MgMotivos.StartAsync(TServer.MySql),
                        Migrate.MgMunicipios.StartAsync(TServer.MySql),
                        Migrate.MgNatureza.StartAsync(TServer.MySql),
                        Migrate.MgPaises.StartAsync(TServer.MySql),
                        Migrate.MgQualifica.StartAsync(TServer.MySql));

                    await Migrate.MgEstabelecimentos.StartAsync(TServer.MySql);
                    await Migrate.MgEmpresas.StartAsync(TServer.MySql);
                    await Migrate.MgSocios.StartAsync(TServer.MySql);
                    await Migrate.MgSimples.StartAsync(TServer.MySql);
                    break;
                case 3:
                    Helpers.DataBase.CreateInSqlServer();
                    await Helpers.CreateTables.StartAsync(TServer.SqlServer);
                    break;
                case 4:
                    Helpers.DataBase.CreateInMySql();
                    await Helpers.CreateTables.StartAsync(TServer.MySql);
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
