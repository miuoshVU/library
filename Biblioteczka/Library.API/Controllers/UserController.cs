using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("/User")]
    public class UserController : Controller
    {
        private readonly IUserService _UserService;
        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUser()
        {
            var books = _UserService.GetAllUser();

            return Ok(books);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddUser([FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = _UserService.CreateUser(dto);
            if (User is null)
                return StatusCode(500);
            return Ok();

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var isDeleted = _UserService.DeleteUser(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();

        }

        [Authorize(Roles = "user, admin")]
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateUserDto dto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _UserService.UpdateUser(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
