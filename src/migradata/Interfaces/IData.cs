namespace migradata.Interfaces;

public interface IData
{
    IEnumerable<string>? CNPJBase { get; set; }
    void ClearParameters();
    void AddParameters(string parameterName, object parameterValue);
    Task ReadAsync(string query);
    Task WriteAsync(string query);
    void CheckDB();
}