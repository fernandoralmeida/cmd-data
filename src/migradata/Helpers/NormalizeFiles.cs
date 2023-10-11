using System.IO.Compression;
using System.IO;
using MongoDB.Driver;

namespace migradata.Helpers;

public class NormalizeFiles
{
    public async Task Start(string path)
    {
        await DeleteNotZip(path);

        var _tasks = new List<Task>();
        var _listfiles = new List<string>();
        var _lists = new List<IEnumerable<string>>();

        foreach (string file in Directory.GetFiles(path))
            if (file.Contains(".zip") == true)
                _listfiles.Add(file);


        int parts = Cpu.Count;
        int size = (_listfiles.Count / parts) + 1;

        for (int i = 0; i < parts; i++)
            _lists.Add(_listfiles.Skip(i * size).Take(size));


        foreach (var rows in _lists)
            _tasks.Add(new Task(async () =>
            {
                foreach (var row in rows)
                    await Unzip(row, path);
            }));

        Parallel.ForEach(_tasks, t => t.Start());
    }

    public async Task<string[]> DoListAync(string path, string extension)
        => await Task.Run(() => Directory.GetFiles(path).Where(s => s.Contains(extension)).ToArray());

    private async Task DeleteNotZip(string sourceFilePath)
        => await Task.Run(() =>
        {
            foreach (string arquivo in Directory.GetFiles(sourceFilePath))
                if (Path.GetExtension(arquivo) != ".zip")
                    File.Delete(arquivo);
        });


    private async Task Unzip(string sourceFilePath, string destinationFolderPath)
        => await Task.Run(() =>
        {
            string filename = Path.GetFileNameWithoutExtension(sourceFilePath);
            string fileextension = Path.GetExtension(sourceFilePath);

            using (ZipArchive archive = ZipFile.OpenRead(sourceFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Log.Storage($"Unzip File:{filename}{fileextension}");
                    string filePath = Path.Combine(destinationFolderPath, entry.FullName);
                    entry.ExtractToFile(filePath, true);

                    File.Move(filePath, Path.Combine(Path.GetDirectoryName(filePath)!, $"{filename}{Path.GetExtension(filePath)}"));
                    Log.Storage($"File:{filename}{Path.GetExtension(filePath)} OK");
                }
            }
        });
}