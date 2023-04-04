using System.Data;
using migradata.Helpers;
using migradata.Interfaces;
using Npgsql;

namespace migradata.Postgres;

public class Data : IData
{
    private readonly string _connectionString = SqlCommands.ConnectionString_PostgreSql;

    private NpgsqlParameterCollection ParameterCollection = new NpgsqlCommand().Parameters;

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new NpgsqlParameter(parameterName, parameterValue));
    }

    public IEnumerable<string>? CNPJBase { get; set; }

    public async Task ReadAsync(string query)
     => await Task.Run(() =>
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = query;
                    _command.CommandTimeout = 0;

                    foreach (NpgsqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                    }

                    NpgsqlDataReader reader = _command.ExecuteReader();

                    var _list = new List<string>();
                    while (reader.Read())
                    {
                        _list.Add(reader[0].ToString()!);
                    }
                    CNPJBase = _list;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                }
            }
        });

    public async Task WriteAsync(string query)
        => await Task.Run(() =>
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        NpgsqlCommand _command = connection.CreateCommand();
                        _command.CommandType = CommandType.Text;
                        _command.CommandText = query;
                        _command.CommandTimeout = 0;

                        foreach (NpgsqlParameter p in ParameterCollection)
                        {
                            _command.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                        }

                        var r = _command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Log.Storage("Error: " + ex.Message);
                    }
                }
            });

    public void CheckDB()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Log.Storage("Successful Connection!");
            }
            catch (Exception ex)
            {
                Log.Storage("Error: " + ex.Message);
            }
        }
    }
}