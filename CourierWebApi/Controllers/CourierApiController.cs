using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierWebApi.Models;
using CourierWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourierWebApi.Controllers {

    [Route("api/couriers")]
    [ApiController]
    public class CourierApiController : ControllerBase {

        private readonly BoxOwlDbContext _context;

        public CourierApiController(BoxOwlDbContext context) {
            _context = context;
        }

        // GET: api/couriers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courier>>> GetCourier() {
            return await _context.Couriers.ToListAsync();
        }

        // GET: api/couriers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourierDto>> GetCourier(int id) {
            var courier = await _context.Couriers
                .Include(x => x.CourierRatings)
                .ThenInclude(x => x.IdRatingNavigation)
                .Where(x => x.IdCourier == id)
                .FirstOrDefaultAsync();

            if (courier == null) {
                return NotFound();
            }

            return Ok(ToCourierDto(courier));
        }

        // PUT: api/couriers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Courier>> PutCourier(int id, Courier courier) {
            if (id != courier.IdCourier) {
                return BadRequest();
            }
            _context.Entry(courier).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!UserExists(id)) {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        private bool UserExists(int id) {
            return _context.Couriers.Any(e => e.IdCourier == id);
        }

        private static CourierDto ToCourierDto(Courier courier) {
            var courierDto = new CourierDto {
                IdCourier = courier.IdCourier,
                CourierName = courier.CourierName,
                CourierSurname = courier.CourierSurname,
                CourierPatronymic = courier.CourierPatronymic,
                CourierPhone = courier.CourierPhone,
                CourierImage = courier.CourierImage,
                CourierPassword = courier.CourierPassword,
                CourierSalt = courier.CourierSalt,
                CourierMoney = courier.CourierMoney,
                CourierRatingCount = courier.CourierRatings.Count,
            };
            var courierRating = courier.CourierRatings.Select(x => x.IdRatingNavigation.RatingScore).ToList();
            if (!courierRating.Any()) {
                courierDto.CourierRating = 0.0m;
            } else {
                courierDto.CourierRating = (decimal)courierRating.Average();
            }
            return courierDto;
        }
    }
}
