using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourierWebApi.Controllers {

    [ApiController]
    public class ChatApiController : ControllerBase {

        private readonly BoxOwlDbContext _context;

        public ChatApiController(BoxOwlDbContext context) {
            _context = context;
        }

        [HttpPost]
        [Route("api/chat/send-message")]
        public async Task<ActionResult<Message>> SaveMessage(Message message) {
            try {
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok(message);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/chat/{orderId}")]
        public ActionResult<List<Message>> GetMessages(int orderId) {
            try {
                var messageList = _context.Messages.Where(x => x.IdOrder == orderId);
                return Ok(messageList);
            } catch (Exception ex) {
                return NotFound(ex.Message);
            }
        }
    }
}
