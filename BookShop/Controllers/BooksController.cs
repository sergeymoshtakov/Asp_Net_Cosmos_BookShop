using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public BooksController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetBooksAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _cosmosDbService.GetBookAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            book.Id = Guid.NewGuid();
            await _cosmosDbService.AddBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cosmosDbService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
