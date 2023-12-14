using System.Data;
using migradata.Models;

namespace migradata.Interfaces;
public interface IDataWrite<T> where T : class
{
    Task WriteAsync(IAsyncEnumerable<T> rows, string database, string datasource);
}