using System.Data;
using System.Data.SqlClient;
using migradata.Helpers;
using migradata.Interfaces;

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

    public async Task<DataTable> ReadAsync(string querySelect, string dbname)
     => await Task.Run(() =>
        {
            using (SqlConnection connection = new ($"{DataBase.DataSource_SqlServer}Database={dbname};"))
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

                    return _table;
                }
                catch (Exception ex)
                {
                    Log.Storage("Error: " + ex.Message);
                    return new DataTable();
                }
            }
        });

    public async Task WriteAsync(string queryWrite, string dbname)
        => await Task.Run(() =>
            {
                using (SqlConnection connection = new ($"{DataBase.DataSource_SqlServer}Database={dbname};"))
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
                        Log.Storage("Error: " + ex.Message);
                    }
                }
            });

    public void CheckDB(string dbname)
    {
        using (SqlConnection connection = new ($"{DataBase.DataSource_SqlServer}Database={dbname};"))
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
