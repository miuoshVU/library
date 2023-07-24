using Microsoft.AspNetCore.Mvc;
using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers
{
    [Route("/ProposedBook")]
    public class ProposedBookController : Controller
    {
        private readonly IProposedBookService _proposedBookService;
        public ProposedBookController(IProposedBookService ProposedBookService)
        {
            _proposedBookService = ProposedBookService;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet]
        public ActionResult<List<ProposedBookDto>> GetAllProposedBook()
        {
            var books = _proposedBookService.GetAllProposedBook();

            return Ok(books);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult AddProposedBook([FromBody] ProposedBookDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ProposedBook = _proposedBookService.CreateProposedBook(dto);
            if (ProposedBook is null)
                return StatusCode(500);
            return Ok();

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _proposedBookService.DeleteProposedBook(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();

        }

        [Authorize(Roles = "user, admin")]
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateProposedBookDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _proposedBookService.UpdateProposedBook(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
 
}
