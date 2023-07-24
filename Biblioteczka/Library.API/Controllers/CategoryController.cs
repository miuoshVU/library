using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAll()
        {
            var books = _CategoryService.GetAllCategory();

            return Ok(books);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddCategory([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _CategoryService.CreateCategory(dto);

            if (category is null)
                return StatusCode(500);

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _CategoryService.DeleteCategory(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();

        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CategoryDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _CategoryService.UpdateCategory(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
     

    }
}
