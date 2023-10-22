using migradata.Helpers;

namespace migradata;
class Program
{
    //private static TServer optserver;

    static async Task Main(string[] args)
    {
        Log.Storage("Starting System");
        while (true)
        {
            Thread.Sleep(1000);
            Console.WriteLine("\nMigraData V1.0");
            Console.WriteLine("Choose Options!");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 Use SqlServer");
            Console.WriteLine("------------------------");
            Console.WriteLine("0 Close");

            Console.Write("Option: ");
            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            Variables.CommandLine = @"c:\\data";
            switch (choice)
            {
                case 1:

                    Console.Write("Directo: ");
                    Variables.CommandLine = Console.ReadLine()!;
                    if (Directory.Exists(Variables.CommandLine))
                    {
                        await Container.MigrateAsync(
                                server: TServer.SqlServer,
                                database: DataBase.MigraData_RFB,
                                datasource: DataSource.SqlServer);
                        break;
                    }
                    else
                        return;

                case 0:
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
