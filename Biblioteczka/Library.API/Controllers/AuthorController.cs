using Library.API.Interface;
using Library.API.Models;
using Library.CodeFirstDatabase.Entities;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize(Roles = "admin, user")]
    [Route("/Author")]
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<List<AuthorDto>> GetAll()
        {
            var authors = _authorService.GetAllAuthors();

            return Ok(authors);
        }

        [HttpPost]
        public ActionResult AddAuthor([FromBody] AuthorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _authorService.CreateAuthor(dto);
            if (author is null)
                return StatusCode(500);
            return Ok();       
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _authorService.DeleteAuthor(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] AuthorDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _authorService.UpdateAuthor(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

