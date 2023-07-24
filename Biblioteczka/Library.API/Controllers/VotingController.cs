using Library.API.Interface;
using Library.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("Voting")]
    public class VotingController : Controller
    {
        private readonly IVotingService _votingService;
        public VotingController(IVotingService votingService)
        {
            _votingService = votingService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult RestartVots()
        {
            var result = _votingService.RestartVoting();
            if (result)
                return Ok();
            return BadRequest();
        }

        [Authorize(Roles = "user")]
        [HttpDelete]
        public ActionResult Vote([FromBody] VoteUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _votingService.Vote(dto);

            if (result)
                return Ok();
            return BadRequest();
        }
    }

    
}
