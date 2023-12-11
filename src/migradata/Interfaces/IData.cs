using System.Data;
using migradata.Models;

namespace migradata.Interfaces;

public interface IData
{
    void ClearParameters();
    void AddParameters(string parameterName, object parameterValue);
    Task<DataTable> ReadAsync(string query, string database, string datasource);
    IAsyncEnumerable<MIndicadoresnet> ReadViewAsync(string query, string database, string datasource);
    Task WriteAsync(DataTable dtable, string tablename, string database, string datasource);
    Task ExecuteAsync(string queryWrite, string database, string datasource);
    Task CreateDB(string datasource, string database, List<MSqlCommand> sqlcommands);
    Task<bool> DbExists(string database, string datasource);
}