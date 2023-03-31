using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace migradata.Helpers;

public static class DataBase
{

    public static void CreteInSqlServer()
    {
        string connectionString = Environment.GetEnvironmentVariable("sqlserver_default")!;
        SqlConnection conn = new SqlConnection(connectionString);

        conn.Open();

        SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM sys.databases WHERE name='{SqlCommands.DataBaseName}'", conn);
        int result = (int)cmd.ExecuteScalar();
        if (result == 0)
        {
            SqlCommand createCmd = new SqlCommand($"CREATE DATABASE {SqlCommands.DataBaseName}", conn);
            if( createCmd.ExecuteNonQuery() == 1)
                Console.WriteLine($"{SqlCommands.DataBaseName} successfully created!");
        }

        conn.Close();
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