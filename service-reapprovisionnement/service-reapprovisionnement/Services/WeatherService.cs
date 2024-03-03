using Reapprovisionnement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using service_reapprovisionnement;
namespace services;

public class WeatherService
{
		private readonly IMongoCollection<WeatherForecast> _weatherCollection;

		public WeatherService(DatabaseService databaseService) {
			_weatherCollection = databaseService.mongoDatabase.GetCollection<WeatherForecast>("Weathers");
		}

		public async Task<List<WeatherForecast>> GetAsync() =>
        await _weatherCollection.Find(_ => true).ToListAsync();

    public async Task<WeatherForecast?> GetAsync(string id) =>
        await _weatherCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(WeatherForecast newBook) =>
        await _weatherCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, WeatherForecast updatedBook) =>
        await _weatherCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _weatherCollection.DeleteOneAsync(x => x.Id == id);
}