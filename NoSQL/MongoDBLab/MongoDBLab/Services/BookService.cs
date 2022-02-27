using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDBLab.Models;

namespace MongoDBLab.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public async Task<List<Book>> Query()
        {
            var result = await _books.Find(book => book.CreationTime > DateTime.Now).ToListAsync();
            return result;
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public async Task<UpdateResult> UpdatePrice()
        {
            // 使用 json 更新
            return await _books.UpdateManyAsync(s => s.Price < 20, new JsonUpdateDefinition<Book>("{$set: { \"Price\": 435}}"));

            //return await _books.UpdateManyAsync(s => s.Price < 20, new ObjectUpdateDefinition<Book>(new
            //{
            //    Price = 455
            //}));

            //_books.UpdateManyAsync(s => s.Price < 20, new BsonDocumentUpdateDefinition<Book>())
        }

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
