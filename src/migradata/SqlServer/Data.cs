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

    public async Task WriteAsync(DataTable dtable, string tablename, string database, string datasource)
    {
        using (SqlConnection connection = new($"{datasource}Database={database};"))
        {
            try
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new(connection))
                {
                    bulkCopy.DestinationTableName = tablename;
                    bulkCopy.BulkCopyTimeout = 0;
                    await bulkCopy.WriteToServerAsync(dtable);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                Log.Storage("Error: " + ex.Message);
            }
        };
    }

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
    /// <param name="database">DataBase Name</param>
    /// <param name="sqlcommands">Lista de comandos SQL, ex: criar tabelas ou vews via scriptsql</param>
    /// <returns></returns>
    public async Task CreateDB(string datasource, string database, List<MSqlCommand> sqlcommands)
    => await Task.Run(() =>
    {
        using SqlConnection connection = new($"{datasource}");
        connection.Open();

        var cmd = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{database}'", connection);

        var databaseId = cmd.ExecuteScalar();

        if (databaseId == null)
        {
            cmd = new SqlCommand($"CREATE DATABASE {database}", connection);
            if (cmd.ExecuteNonQuery() < 1)
                Log.Storage($"{database} successfully created!");

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

    public async Task ExecuteAsync(string queryWrite, string database, string datasource)
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

                await _command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Log.Storage("Error: " + ex.Message);
            }
        };
    }


    public async IAsyncEnumerable<MIndicadoresnet> ReadViewAsync(string query, string database, string datasource)
    {
        using (SqlConnection connection = new($"{datasource}Database={database};"))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(query, connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    yield return new MIndicadoresnet()
                    {
                        CNPJ = reader["CNPJ"] == null ? reader["CNPJ"].ToString() : "",
                        RazaoSocial = reader["RazaoSocial"].ToString(),
                        NaturezaJuridica = reader["NaturezaJuridica"].ToString(),
                        CapitalSocial = Convert.ToDecimal(reader["CapitalSocial"]),
                        PorteEmpresa = reader["PorteEmpresa"].ToString(),
                        IdentificadorMatrizFilial = reader["IdentificadorMatrizFilial"].ToString(),
                        NomeFantasia = reader["NomeFantasia"].ToString(),
                        SituacaoCadastral = reader["SituacaoCadastral"].ToString(),
                        DataSituacaoCadastral = reader["DataSituacaoCadastral"].ToString(),
                        DataInicioAtividade = reader["DataInicioAtividade"].ToString(),
                        CnaeFiscalPrincipal = reader["CnaeFiscalPrincipal"].ToString(),
                        CnaeDescricao = reader["CnaeDescricao"].ToString(),
                        CEP = reader["CEP"].ToString(),
                        Logradouro = reader["Logradouro"].ToString(),
                        Numero = reader["Numero"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        UF = reader["UF"].ToString(),
                        Municipio = reader["Municipio"].ToString(),
                        OpcaoSimples = reader["OpcaoSimples"].ToString(),
                        DataOpcaoSimples = reader["DataOpcaoSimples"].ToString(),
                        DataExclusaoSimples = reader["DataExclusaoSimples"].ToString(),
                        OpcaoMEI = reader["OpcaoMEI"].ToString(),
                        DataOpcaoMEI = reader["DataOpcaoMEI"].ToString(),
                        DataExclusaoMEI = reader["DataExclusaoMEI"].ToString()
                    };
                }
            }
        }
    }
}
