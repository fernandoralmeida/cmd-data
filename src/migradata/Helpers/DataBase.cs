using System.Data.SqlClient;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using Npgsql;

namespace migradata.Helpers;

public static class DataBase
{
    public static readonly string MigraData_RFB = @"MigraData_RFB";

    public static readonly string Sim_RFB_db20210001 = @"Sim-RFB-db20210001";

    public static readonly string IndicadoresNET = @"www_indicadores";

    public static readonly string DataSource_SqlServer
        = Environment.GetEnvironmentVariable("datasource_sqlserver")!;

    public static readonly string DataSource_Docker_SqlServer
        = Environment.GetEnvironmentVariable("datasource_sqlserver")!;

    public static string DataSource_MySQL
        = Environment.GetEnvironmentVariable("datasource_mysql")!;

    public static string DataSource_PostgreSql
        = Environment.GetEnvironmentVariable("datasource_postgresql")!;

    public static void CreateInSqlServer(string dbname)
    {
        using SqlConnection connection = new ($"{DataSource_Docker_SqlServer}Database={dbname};");
        connection.Open();

        var cmd = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{dbname}'", connection);

        var databaseId = cmd.ExecuteScalar();

        if (databaseId == null)
        {
            cmd = new SqlCommand($"CREATE DATABASE {dbname}", connection);
            if (cmd.ExecuteNonQuery() < 1)
                Log.Storage($"{dbname} successfully created!");
        }

        connection.Close();
    }

    public static void CreateInMySql(string dbname)
    {
        MySqlConnection connection = new MySqlConnection($"{DataSource_MySQL}Database={dbname};");
        connection.Open();

        MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {dbname};", connection);
        if (command.ExecuteNonQuery() == 1)
            Log.Storage($"{dbname} successfully created!");

        connection.Close();
    }

    public static void CreateInPostgreSql(string dbname)
    {
        string connectionString = $"{DataSource_PostgreSql}Database={dbname};";
        try
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Log.Storage($"{dbname} OK!");
        }
        catch
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"CREATE DATABASE {dbname}", conn))
                    {
                        cmd.ExecuteScalar();
                        Log.Storage($"{dbname} successfully created!");
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