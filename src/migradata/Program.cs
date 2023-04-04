using migradata.Helpers;

namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {
        Log.Storage("Starting System");
        while (true)
        {
            Thread.Sleep(1000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Choose Options!");
            Console.WriteLine("----------");
            Console.WriteLine("1. Migrate To SqlServer");
            Console.WriteLine("2. Migrate To MySql");
            Console.WriteLine("3. Create In SqlServer");
            Console.WriteLine("4. Create In MySql");
            Console.WriteLine("5. Create In PostgreSQL");
            Console.WriteLine("----------");
            Console.WriteLine("6. Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Log.Storage("Normalize Files");
                    await new NormalizeFiles().Start(@"C:\data");
                    await Migrate.MgNormalize.StartAsync(TServer.SqlServer);
                    Log.Storage("Start migration...");
                   
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
                    
                    await Log.Write(Log.StorageLog!);
                    break;
                case 2:
                    Log.Storage("Normalize Files");
                    await new NormalizeFiles().Start(@"C:\data");
                    await Migrate.MgNormalize.StartAsync(TServer.MySql);
                    Log.Storage("Start migration...");

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
                    
                    await Log.Write(Log.StorageLog!);
                    break;
                case 3:
                    Helpers.DataBase.CreateInSqlServer();
                    await Helpers.CreateTables.StartAsync(TServer.SqlServer);
                    
                    await Log.Write(Log.StorageLog!);
                    break;
                case 4:
                    Helpers.DataBase.CreateInMySql();
                    await Helpers.CreateTables.StartAsync(TServer.MySql);
                    
                    await Log.Write(Log.StorageLog!);
                    break;
                case 5:
                    Helpers.DataBase.CreateInPostgreSql();
                    await Helpers.CreateTables.StartAsync(TServer.PostgreSql);
                    break;
                case 6:
                    Log.Storage("Closing App...");
                    return;
                default:
                    Log.Storage("Invalid option. Try again!");
                    await Log.Write(Log.StorageLog!);
                    break;
            }
        }
    }
}
