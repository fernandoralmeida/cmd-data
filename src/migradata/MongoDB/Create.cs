using migradata.Helpers;
using migradata.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace migradata.MongoDB;

public class Create
{
    public async Task DatabaseIfNotExists(string databaseName, string collection)
    => await Task.Run(() =>
    {

        var client = new MongoClient("mongodb://127.0.0.1:27017");
        var dbList = client.ListDatabases().ToList();

        bool databaseExists = dbList.Any(db => db.Contains(databaseName.ToLower()));

        if (!databaseExists)
        {
            var database = client.GetDatabase(DataBase.MigraData_RFB.ToLower());
            Log.Storage($"{DataBase.MigraData_RFB} successfully created!");
            var collectionExists = database
                .ListCollections(
                new ListCollectionsOptions
                {
                    Filter = Builders<BsonDocument>
                    .Filter.Eq("name", collection)
                })
                .Any();
            if (!collectionExists)
            {
                database.CreateCollection(collection);
                Log.Storage($"{collection} successfully created!");
            }
        }

        foreach (var item in client.ListDatabases().ToList())
        {
            Log.Storage(item.ToString());
        }

        //var _data = client.GetDatabase(SqlCommands.DataBaseName.ToLower());
        //var _estabelecimento = _data.GetCollection<MEstabelecimento>(collection);

        //_estabelecimento.InsertOne(new MEstabelecimento());

        client = null;

    });

}