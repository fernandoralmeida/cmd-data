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

    public async Task ReadAsync(string querySelect)
     => await Task.Run(() =>
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexão bem-sucedida!");

                    //SqlCommand commandSelect = new SqlCommand(querySelect, connection);
                    SqlCommand _command = connection.CreateCommand();
                    _command.CommandType = CommandType.Text;
                    _command.CommandText = querySelect;

                    foreach (SqlParameter p in ParameterCollection)
                    {
                        _command.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                    }

                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"CNPJ: {reader[1]}, Nome: {reader[2]}, Natureza: {reader[3]}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
            }
        });

    public async Task<bool> WriteAsync(string queryWrite)
 => await Task.Run(() =>
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexão bem-sucedida!");

                //SqlCommand commandSelect = new SqlCommand(querySelect, connection);
                SqlCommand _command = connection.CreateCommand();
                _command.CommandType = CommandType.Text;
                _command.CommandText = queryWrite;
                _command.CommandTimeout = 0;

                foreach (SqlParameter p in ParameterCollection)
                {
                    _command.Parameters.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                Console.WriteLine("Executando!");
                var r = _command.ExecuteNonQuery();

                Console.WriteLine($"Registros afetados: {r}");
                if ( r > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
                return false;
            }
        }
    });

}
