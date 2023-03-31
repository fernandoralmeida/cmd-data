using System.Data;
using migradata.Helpers;
using MySql.Data.MySqlClient;

namespace migradata.MySql;

public static class Data
{
    private static readonly string _connectionString = SqlCommands.ConnectionString_MySql;

    private static MySqlParameterCollection ParameterCollection = new MySqlCommand().Parameters;

    public static void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public static void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new MySqlParameter(parameterName, parameterValue));
    }

    public static IEnumerable<string>? CNPJBase { get; set; }

    public static async Task ReadAsync(string querySelect)
     => await Task.Run(() =>
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = querySelect;
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
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
            }
        });

    public static async Task WriteAsync(string queryWrite)
        => await Task.Run(() =>
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand _command = connection.CreateCommand();
                        _command.CommandType = CommandType.Text;
                        _command.CommandText = queryWrite;
                        _command.CommandTimeout = 0;

                        foreach (MySqlParameter p in ParameterCollection)
                        {
                            _command.Parameters.Add(new MySqlParameter(p.ParameterName, p.Value));
                        }

                        var r = _command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao conectar: " + ex.Message);
                    }
                }
            });

    public static void CheckDB()
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexão bem-sucedida!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao SQL Server: " + ex.Message);
            }
        }
    }
}