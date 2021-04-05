using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CourierWebApi.Utils.Utils;

namespace CourierWebApi.Controllers {

    [ApiController]
    public class AuthApiController : ControllerBase {
        private readonly BoxOwlDbContext _context;
        public AuthApiController(BoxOwlDbContext context) {
            _context = context;

        }

        [HttpPost]
        [Route("api/couriers/register")]
        public async Task<ActionResult<bool>> CreateCourier(Courier courier) {
            if (!_context.Couriers.Any(x => x.CourierPhone == courier.CourierPhone)) {
                try {
                    var courierDb = new Courier {
                        CourierName = courier.CourierName,
                        CourierSurname = courier.CourierSurname,
                        CourierPatronymic = courier.CourierPatronymic,
                        CourierPhone = courier.CourierPhone,
                        CourierSalt = Convert.ToBase64String(GetRandomSalt(16))
                    };
                    courierDb.CourierPassword = Convert.ToBase64String(SaltHashPassword(
                        Encoding.ASCII.GetBytes(courier.CourierPassword),
                        Convert.FromBase64String(courierDb.CourierSalt)));
                    await _context.Couriers.AddAsync(courierDb);
                    await _context.SaveChangesAsync();
                    return Ok(true);
                } catch (Exception ex) {
                    return NotFound(ex.Message);
                }
            }
            return Ok(false);
        }

        [HttpPost]
        [Route("api/couriers/login")]
        public async Task<ActionResult<Courier>> LoginCourier(Courier courier) {
            if (_context.Couriers.Any(x => x.CourierPhone == courier.CourierPhone)) {
                var courierDb = await _context.Couriers.FirstOrDefaultAsync(x => x.CourierPhone == courier.CourierPhone);
                var courierPostHash = Convert.ToBase64String(
                    SaltHashPassword(Encoding.ASCII.GetBytes(courier.CourierPassword),
                        Convert.FromBase64String(courierDb.CourierSalt)));
                if (courierPostHash == courierDb.CourierPassword) {
                    return Ok(courierDb);
                }
            }
            return NoContent();
        }
    }
}
