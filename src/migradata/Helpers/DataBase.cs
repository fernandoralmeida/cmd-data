using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace migradata.Helpers;

public static class DataBase
{

    public static void CreateInSqlServer()
    {

        string connectionString = Environment.GetEnvironmentVariable("sqlserver_default")!;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var cmd = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{SqlCommands.DataBaseName}'", connection);

            var databaseId = cmd.ExecuteScalar();

            if (databaseId == null)
            {
                cmd = new SqlCommand($"CREATE DATABASE {SqlCommands.DataBaseName}", connection);
                if (cmd.ExecuteNonQuery() < 1)
                    Console.WriteLine($"{SqlCommands.DataBaseName} successfully created!");
            }            

            connection.Close();
        }        
    }

    public static void CreateInMySql()
    {
        string connectionString = Environment.GetEnvironmentVariable("mysql_default")!;
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {SqlCommands.DataBaseName};", connection);
        if (command.ExecuteNonQuery() == 1)
            Console.WriteLine($"{SqlCommands.DataBaseName} successfully created!");

        connection.Close();
    }

}