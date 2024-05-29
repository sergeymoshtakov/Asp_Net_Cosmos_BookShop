using BookShop.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _authorsContainer;
        private Container _booksContainer;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName)
        {
            _authorsContainer = cosmosDbClient.GetContainer(databaseName, "AuthorsContainer");
            _booksContainer = cosmosDbClient.GetContainer(databaseName, "BooksContainer");
        }

        // Author operations
        public async Task AddAuthorAsync(Author author)
        {
            await _authorsContainer.CreateItemAsync(author, new PartitionKey(author.Id.ToString()));
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            await _authorsContainer.DeleteItemAsync<Author>(id.ToString(), new PartitionKey(id.ToString()));
        }

        public async Task<Author> GetAuthorAsync(Guid id)
        {
            try
            {
                var response = await _authorsContainer.ReadItemAsync<Author>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(string query)
        {
            var queryDefinition = new QueryDefinition(query);
            var authors = new List<Author>();

            var iterator = _authorsContainer.GetItemQueryIterator<Author>(queryDefinition);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                authors.AddRange(response.ToList());
            }

            return authors;
        }

        public async Task UpdateAuthorAsync(Guid id, Author author)
        {
            await _authorsContainer.UpsertItemAsync(author, new PartitionKey(id.ToString()));
        }

        // Book operations
        public async Task AddBookAsync(Book book)
        {
            await _booksContainer.CreateItemAsync(book, new PartitionKey(book.Id.ToString()));
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _booksContainer.DeleteItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()));
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            try
            {
                var response = await _booksContainer.ReadItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(string query)
        {
            var queryDefinition = new QueryDefinition(query);
            var books = new List<Book>();

            var iterator = _booksContainer.GetItemQueryIterator<Book>(queryDefinition);
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                books.AddRange(response.ToList());
            }

            return books;
        }

        public async Task UpdateBookAsync(Guid id, Book book)
        {
            await _booksContainer.UpsertItemAsync(book, new PartitionKey(id.ToString()));
        }
    }
}
