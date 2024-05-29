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
    public class AuthorsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public AuthorsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetAuthorsAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _cosmosDbService.GetAuthorAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            author.Id = Guid.NewGuid();
            await _cosmosDbService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateAuthorAsync(id, author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cosmosDbService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
