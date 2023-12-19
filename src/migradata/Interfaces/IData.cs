using System.Data;
using migradata.Models;

namespace migradata.Interfaces;

public interface IData
{
    IEnumerable<string>? CNPJBase { get; set; }
    void ClearParameters();
    void AddParameters(string parameterName, object parameterValue);
    Task<DataTable> ReadAsync(string query, string dbname, string datasource);
    Task WriteAsync(string query, string dbname, string datasource);
    Task CreateDB(string datasource, string dbname, List<MSqlCommand> sqlcommands);
    Task<bool> DbExists(string dbname, string datasource);
    IAsyncEnumerable<MIndicadoresnet> ReadViewAsync(string query, string database, string datasource);
}