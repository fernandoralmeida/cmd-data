using System.Configuration;
using System.IO.Compression;
using migradata.Helpers;
using migradata.MySql;
using MongoDB.Driver.Core.Operations;

namespace migradata;
class Program
{

    private enum ESteps { Step1 = 1, Step2 = 2, Step3 = 3 }
    private static ESteps _steps;
    private static TServer optserver;

    static async Task Main(string[] args)
    {
        _steps = ESteps.Step1;
        Log.Storage("Starting System");
        while (true)
        {
            Thread.Sleep(1000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Choose Options!");
            Console.WriteLine("------------------------");

            if (_steps == ESteps.Step1)
            {
                Console.WriteLine("1 Use SqlServer");
                Console.WriteLine("2 Use MySql");
                Console.WriteLine("3 Use PostgreSQL");
                Console.WriteLine("4 Use MongoDB");
            }
            else if (_steps == ESteps.Step2)
            {
                Console.WriteLine($"Using {optserver}");
                Console.WriteLine("------------------------");
                Console.WriteLine("1 Migrate from file");
                Console.WriteLine("2 Migrate from Database");
            }
            else
            {
                Console.WriteLine("Migration Completed");
            }

            Console.WriteLine("------------------------");
            Console.WriteLine("0 Close");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);
            switch (choice)
            {
                case 1:

                    if (_steps == ESteps.Step1)
                    {
                        DataBase.CreateInSqlServer(DataBase.MigraData_RFB, DataSource.SqlServer);
                        _steps = ESteps.Step2;
                        optserver = TServer.SqlServer;
                    }
                    else
                    {
                        _steps = ESteps.Step3;

                        if (optserver == TServer.MongoDB)
                        {
                            await new MongoDB.Create().DatabaseIfNotExists(DataBase.MigraData_RFB, "estabelecimentos");
                            return;
                        }

                        await Container.MigrateFromFileAsync(optserver, DataBase.MigraData_RFB, DataSource.SqlServer);
                    }
                    break;

                case 2:

                    if (_steps == ESteps.Step1)
                    {
                        DataBase.CreateInMySql(DataBase.MigraData_RFB, DataSource.MySQL);
                        _steps = ESteps.Step2;
                        optserver = TServer.MySql;
                    }
                    else
                    {
                        _steps = ESteps.Step3;

                        if (optserver == TServer.MongoDB)
                        {
                            await new MongoDB.Create().DatabaseIfNotExists(DataBase.MigraData_RFB, "estabelecimentos");
                            return;
                        }

                        DataBase.CreateInSqlServer(DataBase.IndicadoresNET, DataSource.Docker_SqlServer_VPS);
                        await Container.DatabaseToDatabaseAsync(TServer.SqlServer,
                                                                DataBase.Sim_RFB_db20210001,
                                                                DataSource.SqlServer,
                                                                DataBase.IndicadoresNET,
                                                                DataSource.Docker_SqlServer_VPS);
                    }
                    break;

                case 3:
                    await Container.MigrateFromFileAsync(TServer.PostgreSql, DataBase.MigraData_RFB, DataSource.PostgreSql);
                    _steps = ESteps.Step2;
                    optserver = TServer.PostgreSql;
                    break;

                case 4:
                    await new MongoDB.Create().DatabaseIfNotExists(DataBase.MigraData_RFB, "estabelecimentos");
                    _steps = ESteps.Step2;
                    optserver = TServer.MongoDB;
                    break;

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
