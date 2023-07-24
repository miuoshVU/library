using Library.API.Interface;
using Library.API.Models;
using Library.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("/BookInstance")]
    public class BookInstanceController : Controller
    {
        private readonly IBookInstancesService _bookInstancesService;
        public BookInstanceController(IBookInstancesService bookInstancesService)
        {
            _bookInstancesService = bookInstancesService;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("GetAll")]
        public ActionResult<List<BookInstanceDto>> GetAll()
        {
            var bookInstances = _bookInstancesService.GetAll();
            return Ok(bookInstances);
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet("{bookId}")]
        public ActionResult<List<BookInstanceDto>> GetByBook([FromRoute] int bookId)
        {
            var instances = _bookInstancesService.GetAllBook(bookId);
            return Ok(instances);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<bool> CreateBookInstance([FromBody] NewBookInstanceDto dto)
        {
            var res = _bookInstancesService.AddBookInstance(dto);
            return Ok(res);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{instanceId}")]
        public ActionResult<bool> UpdateBookInstance([FromBody] UpdateBookInstanceDto dto, [FromRoute] int instanceId)
        {
            var res = _bookInstancesService.UpdateBookInstance(instanceId, dto);
            return Ok(res);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteInstance([FromRoute] int id)
        {
            var res = _bookInstancesService.RemoveBookInstance(id);
            return Ok(res);
        }
    }
}
