using Microsoft.Extensions.Logging;

namespace migradata.Helpers;

public static class Log 
{
    private static List<string> _storagelog = new();
    public static List<string>? StorageLog
    {
        get { return _storagelog; }
        set { _storagelog = value!; }
    }

    public static void Storage(string message)
    {
        StorageLog!.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}");
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}");
    }

    public async static Task Write(IEnumerable<string> messages)
        => await Task.Run(() =>
        {
            try
            {
                foreach (var message in messages)
                {
                    string _file = "log.txt";
                    if (File.Exists(_file) == true)
                        using (StreamWriter sw = File.AppendText(_file))
                            sw.WriteLine(message);
                    else
                        using (StreamWriter writer = File.CreateText(_file))
                            writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Error | {ex.Message}");
            }
            finally
            {
                StorageLog!.Clear();
            }

        });
}