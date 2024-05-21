using MongoDB.Driver;
using API.Models;
using System.Collections.Generic;

namespace API.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>("Products");
        }

        public List<Product> Get() => _products.Find(product => true).ToList();

        public Product Get(string id) => _products.Find<Product>(product => product.Id == id).FirstOrDefault();

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(string id, Product productIn) => _products.ReplaceOne(product => product.Id == id, productIn);

        public void Remove(Product productIn) => _products.DeleteOne(product => product.Id == productIn.Id);

        public void Remove(string id) => _products.DeleteOne(product => product.Id == id);
    }
}