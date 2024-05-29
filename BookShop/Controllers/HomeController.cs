using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public HomeController(ILogger<HomeController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await SeedDatabaseAsync(); // Добавление данных, если они отсутствуют
                var authors = await _cosmosDbService.GetAuthorsAsync("SELECT * FROM c");
                return View(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving data from the database.");
                return View("Error");
            }
        }

        private async Task SeedDatabaseAsync()
        {
            // Проверка, есть ли уже авторы в базе данных Cosmos DB
            var authors = await _cosmosDbService.GetAuthorsAsync("SELECT * FROM c");
            if (!authors.Any())
            {
                // Если нет, то создаем тестовых авторов и книги
                var author1 = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Author1 Name",
                    Surname = "Author1 Surname",
                    Description = "Description for Author1",
                    Books = new List<Book>
                    {
                        new Book { Id = Guid.NewGuid(), Name = "Book1", Price = 10.0f, Decription = "Description for Book1" },
                        new Book { Id = Guid.NewGuid(), Name = "Book2", Price = 12.0f, Decription = "Description for Book2" },
                        new Book { Id = Guid.NewGuid(), Name = "Book3", Price = 14.0f, Decription = "Description for Book3" }
                    }
                };

                var author2 = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Author2 Name",
                    Surname = "Author2 Surname",
                    Description = "Description for Author2",
                    Books = new List<Book>
                    {
                        new Book { Id = Guid.NewGuid(), Name = "Book4", Price = 16.0f, Decription = "Description for Book4" },
                        new Book { Id = Guid.NewGuid(), Name = "Book5", Price = 18.0f, Decription = "Description for Book5" }
                    }
                };

                // Добавляем авторов в базу данных Cosmos DB
                await _cosmosDbService.AddAuthorAsync(author1);
                await _cosmosDbService.AddAuthorAsync(author2);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
