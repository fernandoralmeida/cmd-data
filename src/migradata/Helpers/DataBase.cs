using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace migradata.Helpers;

public static class DataBase
{
    public static readonly string MigraData_RFB = @"MigraData_RFB";

    public static readonly string Sim_RFB_db20210001 = @"Sim-RFB-db20210001";

    public static readonly string IndicadoresNET = @"www_indicadores";

    public static async void CreateInSqlServer(string database, string datasource)
    {
        using SqlConnection connection = new ($"{datasource}");
        connection.Open();

        var cmd = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{database}'", connection);

        var databaseId = cmd.ExecuteScalar();

        if (databaseId == null)
        {
            cmd = new SqlCommand($"CREATE DATABASE {database}", connection);
            if (cmd.ExecuteNonQuery() < 1)
                Log.Storage($"{database} successfully created!");
        }
        connection.Close();

        await CreateTablesAsync(TServer.SqlServer, database, datasource);
    }

    public static async void CreateInMySql(string database, string datasource)
    {
        MySqlConnection connection = new($"{datasource}");
        connection.Open();

        MySqlCommand command = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {database};", connection);
        if (command.ExecuteNonQuery() == 1)
            Log.Storage($"{database} successfully created!");

        connection.Close();

        await CreateTablesAsync(TServer.MySql, database, datasource);
    }

    public static async void CreateInPostgreSql(string database, string datasource)
    {
        string connectionString = $"{datasource}Database={database};";
        try
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Log.Storage($"{database} OK!");
        }
        catch
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"CREATE DATABASE {database}", conn))
                    {
                        cmd.ExecuteScalar();
                        Log.Storage($"{database} successfully created!");
                    }
                }
                
                await CreateTablesAsync(TServer.PostgreSql, database, datasource);
            }
            catch (Exception ex)
            {
                Log.Storage($"{ex.Message}: Database OK!");
            }
        }
    }

    private static async Task CreateTablesAsync(TServer server, string database, string datasource)
    => await Task.Run(async () =>
    {
        if (server == TServer.SqlServer)
            await new SqlServer.Data().WriteAsync(SqlScript.Default, database, datasource);

        if (server == TServer.MySql)
            await new MySql.Data().WriteAsync(SqlScript.MariaDB, database, datasource);

        if (server == TServer.PostgreSql)
            await new Postgres.Data().WriteAsync(SqlScript.Default, database, datasource);
    });
}