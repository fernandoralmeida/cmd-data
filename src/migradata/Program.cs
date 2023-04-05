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
            Console.WriteLine("------------------------");
            Console.WriteLine("1 Create in SqlServer");
            Console.WriteLine("2 Migrate To SqlServer");
            Console.WriteLine("------------------------");
            Console.WriteLine("3 Create In MySql");
            Console.WriteLine("4 Migrate To MySql");
            Console.WriteLine("------------------------");
            Console.WriteLine("5 Create In PostgreSQL");
            Console.WriteLine("6 Migrate To PostgreSQL");
            Console.WriteLine("------------------------");
            Console.WriteLine("7 Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:
                    Helpers.DataBase.CreateInSqlServer();
                    await Helpers.CreateTables.StartAsync(TServer.SqlServer);
                    break;
                case 2:
                    await Container.Execute(TServer.SqlServer);
                    break;
                case 3:
                    Helpers.DataBase.CreateInMySql();
                    await Helpers.CreateTables.StartAsync(TServer.MySql);
                    break;
                case 4:
                    await Container.Execute(TServer.MySql);
                    break;
                case 5:
                    Helpers.DataBase.CreateInPostgreSql();
                    await Helpers.CreateTables.StartAsync(TServer.PostgreSql);
                    break;
                case 6:
                    await Container.Execute(TServer.PostgreSql);
                    return;
                case 7:
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
