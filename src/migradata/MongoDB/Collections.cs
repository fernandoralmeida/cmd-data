using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace migradata.MongoDB;

public static class Collections
{
    public static async Task Estabelecimentos(TCollection collection)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;
        long c3 = 0;

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".ESTABELE"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MEstabelecimento>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        foreach (var item in MMunicipio.MicroRegionJau()
                                                        .Where(s => s == fields[20]
                                                        .ToString()
                                                        .Replace("\"", "")))
                            _list.Add(Documents.Estabelecimento(fields));

                        c1++;
                    }
                }

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MEstabelecimento>>();

                int parts = Cpu.Count;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count()} -> {parts} : {size}");

                foreach (var rows in _lists)
                    _tasks.Add(Task.Run(async () =>
                    {
                        var i = 0;
                        var client = new MongoClient("mongodb://127.0.0.1:27017");
                        var _data = client.GetDatabase(SqlCommands.DataBaseName.ToLower());
                        var _estabelecimento = _data.GetCollection<MEstabelecimento>(collection.ToString().ToLower());
                        await _estabelecimento.InsertManyAsync(rows);
                        i = rows.Count();
                        c2 += i;
                    }));

                await Task.WhenAll(_tasks);

                _innertimer.Stop();

                Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }
            _timer.Stop();
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var _data = client.GetDatabase(SqlCommands.DataBaseName.ToLower());
            var _collection = _data.GetCollection<BsonDocument>(collection.ToString().ToLower());
            c3 = _collection.CountDocuments(FilterDefinition<BsonDocument>.Empty);
            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    });

    public static async Task Empresas(TCollection collection)
    => await Task.Run(async () =>
    {

        int c1 = 0;
        int c2 = 0;
        long c3 = 0;

        var _timer = new Stopwatch();
        _timer.Start();

        try
        {

            foreach (var file in await new NormalizeFiles().DoListAync(@"C:\data", ".EMPRECSV"))
            {
                var _innertimer = new Stopwatch();
                _innertimer.Start();
                var _list = new List<MEmpresa>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    while (!reader.EndOfStream)
                    {
                        c1++;
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');
                        _list.Add(MongoDB.Documents.Empresa(fields));
                    }
                }

                var _mongo = new MongoClient("mongodb://127.0.0.1:27017");
                var _db = _mongo.GetDatabase(SqlCommands.DataBaseName.ToLower());
                var _estabele = _db.GetCollection<MEstabelecimento>(TCollection.Estabelecimentos.ToString().ToLower());
                var projection = Builders<MEstabelecimento>.Projection.Include("CNPJBase").Exclude("_id");
                var _cnpjs = await _estabele.Find(new BsonDocument()).Project(projection).ToListAsync();

                var _tasks = new List<Task>();
                var _lists = new List<IEnumerable<MEmpresa>>();

                int parts = Cpu.Count;
                int size = (_list.Count() / parts) + 1;

                for (int p = 0; p < parts; p++)
                    _lists.Add(_list.Skip(p * size).Take(size));

                Log.Storage($"Migrating: {_list.Count()} -> {parts} : {size}");

                foreach (var rows in _lists)
                    _tasks.Add(Task.Run(async () =>
                    {
                        var client = new MongoClient("mongodb://127.0.0.1:27017");
                        var _data = client.GetDatabase(SqlCommands.DataBaseName.ToLower());
                        var _emp = _data.GetCollection<MEmpresa>(collection.ToString().ToLower());
                        c2 += rows.Count();
                        await _emp.InsertManyAsync(rows);
                    }));

                await Task.WhenAll(_tasks);

                _innertimer.Stop();

                Log.Storage($"Read: {c1} | Migrated: {c2} | Time: {_innertimer.Elapsed.ToString("hh\\:mm\\:ss")}");
            }

            Log.Storage("Analyzing data!");

            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var _data = client.GetDatabase(SqlCommands.DataBaseName.ToLower());
            var _collection = _data.GetCollection<BsonDocument>(collection.ToString().ToLower());
            c3 = _collection.CountDocuments(FilterDefinition<BsonDocument>.Empty);

            _timer.Stop();
            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed.ToString("hh\\:mm\\:ss")}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Error: {ex.Message}");
        }
    });

}