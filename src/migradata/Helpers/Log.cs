namespace migradata.Helpers;

public class Log
{
    public async Task Write(string message)
        => await Task.Run(() =>
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} | {message}");
            string _file = "log.txt";
            if (File.Exists(_file) == true)
                using (StreamWriter sw = File.AppendText(_file))
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} | {message}");
            else
                using (StreamWriter writer = File.CreateText(_file))
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} | {message}");

        });
}