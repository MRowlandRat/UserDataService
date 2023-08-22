using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserDataService.Models;

namespace UserDataService.Data
{
    public class DAL
    {

        private readonly IMongoCollection<UserData> _UserDataCollection;

        public DAL(
        IOptions<PRO250DatabaseSettings> UserDataStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                UserDataStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                UserDataStoreDatabaseSettings.Value.DatabaseName);

            _UserDataCollection = mongoDatabase.GetCollection<UserData>(
                UserDataStoreDatabaseSettings.Value.CollectionName);
        }


        public async Task<List<UserData>> GetAsync() =>
        await _UserDataCollection.Find(_ => true).ToListAsync();

        public async Task<UserData?> GetAsync(Guid id) =>
            await _UserDataCollection.Find(x => x.User_Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserData newUser) =>
            await _UserDataCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(Guid id, UserData updatedData) =>
            await _UserDataCollection.ReplaceOneAsync(x => x.User_Id == id, updatedData);

        public async Task RemoveAsync(Guid id) =>
            await _UserDataCollection.DeleteOneAsync(x => x.User_Id == id);

    }
}
