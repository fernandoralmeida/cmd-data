using System.Data;
using migradata.Models;

namespace migradata.Interfaces;
public interface IDataRead<T> where T : class
{
    IAsyncEnumerable<T> ReadAsync(string query, string database, string datasource);
}