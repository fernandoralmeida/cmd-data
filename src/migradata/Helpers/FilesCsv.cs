using System.IO.Compression;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;

namespace migradata.Helpers;

public static class FilesCsv
{
    public static async Task NormalizeAsync(string path)
    => await Task.Run(async () =>
    {
        var _timer = new Stopwatch();
        _timer.Start();
        await DeleteNotZip(path);

        var _listfiles = new List<string>();

        var _cores = Cpu.Count - 2;

        foreach (string file in Directory.GetFiles(path).OrderBy(s => s))
            if (file.Contains(".zip") == true)
            {
                _listfiles.Add(file);
                Log.Storage(file);
            }

        var _tasks = _listfiles.Select(file => Task.Run(async () => await Unzip(file, path)));

        await Parallel.ForEachAsync(_tasks,
            async (t, _) =>
                await t
            ); 

        _timer.Stop();
        Log.Storage($"Normalized Files! {_timer.Elapsed:hh\\:mm\\:ss}");
    });

    public static async Task<string[]> FilesListAsync(string path, string extension)
    => await Task.Run(() => Directory.GetFiles(path).Where(s => s.Contains(extension)).ToArray());

    private static async Task DeleteNotZip(string sourceFilePath)
    => await Task.Run(() =>
    {
        foreach (string arquivo in Directory.GetFiles(sourceFilePath))
            if (Path.GetExtension(arquivo) != ".zip")
                File.Delete(arquivo);
    });

    private static async Task Unzip(string sourceFilePath, string destinationFolderPath)
    => await Task.Run(() =>
    {
        string filename = Path.GetFileNameWithoutExtension(sourceFilePath);
        string fileextension = Path.GetExtension(sourceFilePath);

        using ZipArchive archive = ZipFile.OpenRead(sourceFilePath);
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string filePath = Path.Combine(destinationFolderPath, entry.FullName);
                entry.ExtractToFile(filePath, true);

                File.Move(filePath, Path.Combine(Path.GetDirectoryName(filePath)!, $"{filename}{Path.GetExtension(filePath)}"));
                Log.Storage($"File: {filename}{Path.GetExtension(filePath)} OK");
            }
        }

    });
}