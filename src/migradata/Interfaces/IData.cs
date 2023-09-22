using System.Data;

namespace migradata.Interfaces;

public interface IData
{
    IEnumerable<string>? CNPJBase { get; set; }
    void ClearParameters();
    void AddParameters(string parameterName, object parameterValue);
    Task<DataTable> ReadAsync(string query, string dbname);
    Task WriteAsync(string query, string dbname);
    void CheckDB(string dbname);
}