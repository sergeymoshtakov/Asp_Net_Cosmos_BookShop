using BookShop.Models;

namespace BookShop.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Book>> GetBooksAsync(string query);
        Task<Book> GetBookAsync(Guid id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Guid id, Book book);
        Task DeleteBookAsync(Guid id);

        Task<IEnumerable<Author>> GetAuthorsAsync(string query);
        Task<Author> GetAuthorAsync(Guid id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Guid id, Author author);
        Task DeleteAuthorAsync(Guid id);
    }

}
