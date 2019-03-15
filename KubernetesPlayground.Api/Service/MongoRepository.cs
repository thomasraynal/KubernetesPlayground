using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KubernetesPlayground.Api.Service
{
    public interface ITestRepository : IBaseMongoRepository
    {
        Task SeedIfNotCreated();
    }

    public class MongoRepository : BaseMongoRepository, ITestRepository
    {

        public MongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
     
        }

        public MongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
         
        }

        public MongoRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
     
        }

        public bool CollectionExists<TDocument>() where TDocument : class, new()
        {
            var collection = GetCollection<ItemDocument>();
            var totalCount = collection.CountDocuments(new BsonDocument());
            return (totalCount > 0) ? true : false;
        }

        public Task SeedIfNotCreated()
        {
            if (!CollectionExists<ItemDocument>())
            {
                AddMany(ItemDocument.Create(10));
            }

            return Task.CompletedTask;
        }
    }
}
