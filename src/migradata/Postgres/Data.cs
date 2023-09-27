using System.Data;
using migradata.Helpers;
using migradata.Interfaces;
using Npgsql;

namespace migradata.Postgres;

public class Data : IData
{
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

    public async Task<DataTable> ReadAsync(string query, string database, string datasource)
     => await Task.Run(() =>
        {
            using (NpgsqlConnection connection = new ($"{datasource}Database={database};"))
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

                    DataTable _table = new();

                    new NpgsqlDataAdapter(_command).Fill(_table);

                    return _table;
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                    return new DataTable();
                }
            }
        });

    public async Task WriteAsync(string query, string database, string datasource)
        => await Task.Run(() =>
            {
                using (NpgsqlConnection connection = new ($"{datasource}Database={database};"))
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

    public void CheckDB(string database, string datasource)
    {
        using (NpgsqlConnection connection = new ($"{datasource}Database={database};"))
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