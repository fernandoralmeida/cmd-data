using System.Data;
using migradata.Helpers;
using migradata.Interfaces;
using MySql.Data.MySqlClient;

namespace migradata.MySql;

public class Data : IData
{
    private readonly string _connectionString = SqlCommands.ConnectionString_MySql;

    private MySqlParameterCollection ParameterCollection = new MySqlCommand().Parameters;

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new MySqlParameter(parameterName, parameterValue));
    }

    public IEnumerable<string>? CNPJBase { get; set; }

    public async Task ReadAsync(string query)
     => await Task.Run(() =>
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = query;
                    _command.CommandTimeout = 0;

                    foreach (MySqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new MySqlParameter(p.ParameterName, p.Value));
                    }

                    MySqlDataReader reader = _command.ExecuteReader();

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
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        });

    public async Task WriteAsync(string query)
        => await Task.Run(() =>
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand _command = connection.CreateCommand();
                        _command.CommandType = CommandType.Text;
                        _command.CommandText = query;
                        _command.CommandTimeout = 0;

                        foreach (MySqlParameter p in ParameterCollection)
                        {
                            _command.Parameters.Add(new MySqlParameter(p.ParameterName, p.Value));
                        }

                        var r = _command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            });

    public void CheckDB()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successful Connection!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}