using System.Data;
using System.Data.SqlClient;
using migradata.Helpers;
using migradata.Interfaces;
using migradata.Models;

namespace migradata.SqlServer;

public class Data : IData
{

    private SqlParameterCollection ParameterCollection = new SqlCommand().Parameters;

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new SqlParameter(parameterName, parameterValue));
    }

    public IEnumerable<string>? CNPJBase { get; set; }

    public async Task<DataTable> ReadAsync(string querySelect, string database, string datasource)
     => await Task.Run(() =>
        {
            using (SqlConnection connection = new($"{datasource}Database={database};"))
            {
                try
                {
                    connection.Open();

                    SqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = querySelect;
                    _command.CommandTimeout = 0;

                    foreach (SqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }

                    DataTable _table = new();

                    new SqlDataAdapter(_command).Fill(_table);
                    connection.Close();
                    return _table;

                }
                catch (Exception ex)
                {
                    connection.Close();
                    Log.Storage("Error: " + ex.Message);
                    return new DataTable();
                }
            }
        });

    public async Task WriteAsync(string queryWrite, string database, string datasource)
        => await Task.Run(() =>
            {
                using (SqlConnection connection = new($"{datasource}Database={database};"))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand _command = connection.CreateCommand();
                        _command.CommandType = CommandType.Text;
                        _command.CommandText = queryWrite;
                        _command.CommandTimeout = 0;

                        foreach (SqlParameter p in ParameterCollection)
                        {
                            _command.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                        }

                        var r = _command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        Log.Storage("Error: " + ex.Message);
                    }
                }
            });

    public async Task<bool> DbExists(string database, string datasource)
    => await Task.Run(() =>
    {
        try
        {
            using SqlConnection connection = new($"{datasource}Database={database};");
            connection.Open();
            Log.Storage("Successful Connection!");
            connection.Close();
            return true;
        }
        catch (Exception ex)
        {
            Log.Storage("Error: " + ex.Message);
            return false;
        }

    });

    /// <summary>
    /// Cria Banco de Dados caso não exista
    /// </summary>
    /// <param name="datasource">DataSource</param>
    /// <param name="dbname">DataBase Name</param>
    /// <param name="sqlcommands">Lista de comandos SQL, ex: criar tabelas ou vews via scriptsql</param>
    /// <returns></returns>
    public async Task CreateDB(string datasource, string dbname, List<MSqlCommand> sqlcommands)
    => await Task.Run(() =>
    {
        using SqlConnection connection = new($"{datasource}");
        connection.Open();

        var cmd = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{dbname}'", connection);

        var databaseId = cmd.ExecuteScalar();

        if (databaseId == null)
        {
            cmd = new SqlCommand($"CREATE DATABASE {dbname}", connection);
            if (cmd.ExecuteNonQuery() < 1)
                Log.Storage($"{dbname} successfully created!");

            Thread.Sleep(3000);

            foreach (var item in sqlcommands)
            {
                cmd = new SqlCommand(item.Command, connection);
                if (cmd.ExecuteNonQuery() < 1)
                    Log.Storage($"{item.Name} successfully created!");

                Thread.Sleep(3000);
            }
        }
        connection.Close();
        Thread.Sleep(3000);
    });

}
