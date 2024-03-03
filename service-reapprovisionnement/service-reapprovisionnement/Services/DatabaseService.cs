using Microsoft.Extensions.Options;
using MongoDB.Driver;
using service_reapprovisionnement;
using Microsoft.Extensions.Configuration;

namespace services;

public class DatabaseService
{
		// Propriétés publiques de l'instance
    public IMongoDatabase mongoDatabase { get; }

		// Constructeur privé pour empêcher l'instanciation externe
		public DatabaseService(IConfiguration configuration)
		{
				string mongoClientUrl = Environment.GetEnvironmentVariable("MONGO_URL") ?? configuration.GetValue<string>("ReapprovisionnementDatabase:ConnectionString");
				string mongoDatabaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? configuration.GetValue<string>("ReapprovisionnementDatabase:DatabaseName");

				var mongoClient = new MongoClient(mongoClientUrl);
        mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
		}
}