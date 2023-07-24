using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("/Spot")]
    public class SpotController : Controller
    {
        private readonly ISpotService _spotService;
        public SpotController(ISpotService spotService)
        {
            _spotService = spotService;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public async Task<ActionResult<List<SpotDto>>> ReadAllSpots()
        {

            var spots = await _spotService.ReadAllSpots();
            return Ok(spots);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("Add")]
        public async Task<ActionResult> AddSpot([FromBody] SpotDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spot = await _spotService.CreateSpot(dto);
            return Ok(dto);
        }
        
        [Authorize(Roles = "admin")]
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult> Update([FromBody] SpotDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await  _spotService.UpdateSpot(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _spotService.DeleteSpot(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();

        }
    }
}
