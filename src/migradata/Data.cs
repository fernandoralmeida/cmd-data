using System.Data;
using System.Data.SqlClient;

namespace migradata;

public class Data
{

    private readonly string _connectionString = Environment.GetEnvironmentVariable("connection_string_migradata")!;

    private SqlParameterCollection ParameterCollection = new SqlCommand().Parameters;

    public void ClearParameters()
    {
        ParameterCollection.Clear();
    }

    public void AddParameters(string parameterName, object parameterValue)
    {
        ParameterCollection.Add(new SqlParameter(parameterName, parameterValue));
    }

    public IEnumerable<string>? CNPJBase {get; set;}

    public async Task ReadAsync(string querySelect)
     => await Task.Run(() =>
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

                    SqlDataReader reader = _command.ExecuteReader();

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

    public async Task WriteAsync(string queryWrite)
        => await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao conectar: " + ex.Message);
                    }
                }
            });

}
