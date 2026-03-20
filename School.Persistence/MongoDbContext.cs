using MongoDB.Driver;

namespace School.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IReadOnlyDictionary<Type, string> _collectionNames;

        public MongoDbContext(string connectionString, string databaseName, IReadOnlyDictionary<Type, string> collectionNames)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            _collectionNames = collectionNames;
        }

        public IMongoCollection<T> GetCollection<T>() where T : class {
            if (_collectionNames.TryGetValue(typeof(T), out var collectionName))
                return _database.GetCollection<T>(collectionName);

            return _database.GetCollection<T>(typeof(T).Name);
         }
    }
}
