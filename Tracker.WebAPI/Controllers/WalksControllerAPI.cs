using Microsoft.AspNetCore.Mvc;
using Tracker.WebAPI.Extentions;
using Tracker.WebAPI.Models;
using Tracker.WebAPI.Services;

namespace Tracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksControllerAPI : ControllerBase
    {
        private readonly WalkService _walkService;
        private readonly WalkingDayService _walkingDayService;

        public WalksControllerAPI()
        {
            _walkService = new WalkService();
            _walkingDayService = new WalkingDayService();
        }

        [HttpGet, Route("/geAllWalks/")]
        public async Task<IActionResult> GetAllWalks(string IMEI)
        {
            var walks = await _walkService.Calculate(IMEI);
            if(walks.Count == 0)
            {
                return NotFound("Incorrect IMEI...");
            }
            return Ok(walks.ClearTrackLocationInCollection());
        }

        [HttpGet, Route("/getAllWalksByDay/")]
        public async Task<IActionResult> GetAllWalksByDay(string IMEI)
        {
            var walks = await _walkService.Calculate(IMEI);
            if (walks.Count == 0)
            {
                return NotFound("Incorrect IMEI...");
            }

            var walkingDays = _walkingDayService.Calculate(walks);

            return Ok(walkingDays.ClearTrackLocationInCollection());
        }
    }
}
