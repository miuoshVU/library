using Library.API.Interface;
using Library.Service;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("/seeder")]
    public class SeederColntroller : Controller
    {
        private readonly ILibrarySeeder _librarySeeder;

        public SeederColntroller(ILibrarySeeder librarySeeder)
        {
            _librarySeeder = librarySeeder;
        }

        [HttpGet]
        public ActionResult<bool> Seed()
        {
            _librarySeeder.BasicSeed();
            return true;
        }
    }
}
