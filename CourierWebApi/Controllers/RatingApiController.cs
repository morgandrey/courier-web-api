using System;
using System.Threading.Tasks;
using CourierWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierWebApi.Controllers
{
    [ApiController]
    public class RatingApiController : ControllerBase {

        private readonly BoxOwlDbContext _context;

        public RatingApiController(BoxOwlDbContext context) {
            _context = context;
        }

        [HttpPost]
        [Route("api/rating")]
        public async Task<ActionResult<Message>> SaveMessage(CourierRating rating) {
            try {
                return Ok(rating);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

    }
}