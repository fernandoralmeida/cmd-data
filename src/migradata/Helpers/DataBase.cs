using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

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
                    Log.Storage($"{SqlCommands.DataBaseName} successfully created!");
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
            Log.Storage($"{SqlCommands.DataBaseName} successfully created!");

        connection.Close();
    }

    public static void CreateInPostgreSql()
    {
        try
        {
            using (var conn = new NpgsqlConnection(SqlCommands.ConnectionString_PostgreSql))
            {
                conn.Open();
                Log.Storage($"{SqlCommands.DataBaseName} OK!");
            }
        }
        catch
        {
            try
            {
                using (var conn = new NpgsqlConnection(Environment.GetEnvironmentVariable("postgresql_default")))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"CREATE DATABASE {SqlCommands.DataBaseName}", conn))
                    {
                        cmd.ExecuteScalar();
                        Log.Storage($"{SqlCommands.DataBaseName} successfully created!");
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Storage($"{ex.Message}: Database OK!");
            }

        }

    }

}