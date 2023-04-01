using System.IO.Compression;
using System.IO;

namespace migradata.Helpers;

public class NormalizeFiles
{
    public async Task Start(string path)
    {
        await DeleteNotZip(path);

        var _tasks = new List<Task>();

        foreach (string file in Directory.GetFiles(path))
            if (file.Contains(".zip") == true)
                _tasks.Add(Unzip(file, path));

        await Task.WhenAll(_tasks);
    }

    public async Task<string[]> DoListAync(string path, string extension)
        => await Task.Run(() => { return Directory.GetFiles(path).Where(s => s.Contains(extension)).ToArray(); });

    private async Task DeleteNotZip(string sourceFilePath)
        => await Task.Run(() =>
        {
            foreach (string arquivo in Directory.GetFiles(sourceFilePath))
                if (Path.GetExtension(arquivo) != ".zip")
                    File.Delete(arquivo);
        });


    private async Task Unzip(string sourceFilePath, string destinationFolderPath)
        => await Task.Run(async () =>
        {
            string filename = Path.GetFileNameWithoutExtension(sourceFilePath);
            string fileextension = Path.GetExtension(sourceFilePath);

            using (ZipArchive archive = ZipFile.OpenRead(sourceFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    await new Log().Write($"Unzip File:{filename}{fileextension}");
                    string filePath = Path.Combine(destinationFolderPath, entry.FullName);
                    entry.ExtractToFile(filePath, true);

                    File.Move(filePath, Path.Combine(Path.GetDirectoryName(filePath)!, $"{filename}{Path.GetExtension(filePath)}"));
                    await new Log().Write($"File:{filename}{Path.GetExtension(filePath)} OK");
                }
            }
        });
}