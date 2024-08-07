using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiProducts.Interface;
using ApiProducts.Domains;

namespace ApiProducts.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName = "Products";

        public ProductRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public async Task<List<Products>> ListarAsync()
        {
            var collection = _database.GetCollection<Products>(_collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<Products> BuscarPorIdAsync(Guid id)
        {
            var collection = _database.GetCollection<Products>(_collectionName);
            return await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }


        public async Task AddAsync(Products product)
        {
            var collection = _database.GetCollection<Products>(_collectionName);
            await collection.InsertOneAsync(product);
        }

        public async Task<Products> AtualizarPorIdAsync(Guid id, Products products)
        {
            var collection = _database.GetCollection<Products>(_collectionName);
            var filter = Builders<Products>.Filter.Eq(p => p.Id, id); // Garantindo que ambos são Guid
            var result = await collection.ReplaceOneAsync(filter, products);
            return result.ModifiedCount > 0 ? products : null;
        }


        public async Task<Products> DeletarPorIdAsync(Guid id, Products products)
        {
            var collection = _database.GetCollection<Products>(_collectionName);
            var filter = Builders<Products>.Filter.Eq(p => p.Id, id); // Garantindo que ambos são Guid
            await collection.DeleteOneAsync(filter);
            return products;
        }

        public Task CadastrarAsync(Products products)
        {
            throw new NotImplementedException();
        }
    }
}
