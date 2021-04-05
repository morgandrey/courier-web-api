using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierWebApi.Models;
using CourierWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourierWebApi.Controllers {

    [ApiController]
    public class OrderApiController : ControllerBase {
        private readonly BoxOwlDbContext _context;
        public OrderApiController(BoxOwlDbContext context) {
            _context = context;

        }

        // GET: api/orders/{orderId}
        [HttpGet]
        [Route("api/orders/{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId) {
            var order = await _context.Orders
                .Include(x => x.IdClientNavigation)
                .Include(x => x.IdOrderStatusNavigation)
                .Include(x => x.ProductOrders)
                .ThenInclude(x => x.IdProductNavigation)
                .FirstOrDefaultAsync(x => x.IdOrder == orderId);
            return Ok(ToOrderDto(order));
        }

        // GET: api/{courierId}/available-orders
        [HttpGet]
        [Route("api/couriers/{courierId}/available-orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAvailableOrders(int courierId) {
            var orders = await _context.Orders
                .Include(x => x.IdClientNavigation)
                .Include(x => x.IdOrderStatusNavigation)
                .Include(x => x.ProductOrders)
                .ThenInclude(x => x.IdProductNavigation)
                .Where(x => x.IdOrderStatus == 1)
                .OrderByDescending(x => x.OrderPrice)
                .ToListAsync();

            return Ok(ToOrderListDTO(orders));
        }

        private static List<OrderDto> ToOrderListDTO(IEnumerable<Order> orders) {
            return orders.Select(order => new OrderDto {
                OrderId = order.IdOrder,
                ClientName = order.IdClientNavigation.ClientName,
                ClientSurname = order.IdClientNavigation.ClientSurname,
                ClientPhone = order.IdClientNavigation.ClientPhone,
                OrderStatusId = order.IdOrderStatusNavigation.IdOrderStatus,
                DeliveryAddress = order.DeliveryAddress,
                OrderDate = order.OrderDate,
                CourierId = order.IdCourier,
                OrderDescription = order.OrderDescription,
                CourierReward = order.CourierReward,
                Products = order.ProductOrders.Select(product => new ProductDto {
                    ProductName = product.IdProductNavigation.ProductName,
                    ProductDescription = product.IdProductNavigation.ProductDescription,
                    ProductPrice = product.IdProductNavigation.ProductPrice
                })
            })
                .ToList();
        }

        private static OrderDto ToOrderDto(Order order) {
            return new() {
                OrderId = order.IdOrder,
                ClientName = order.IdClientNavigation.ClientName,
                ClientSurname = order.IdClientNavigation.ClientSurname,
                ClientPhone = order.IdClientNavigation.ClientPhone,
                OrderStatusId = order.IdOrderStatusNavigation.IdOrderStatus,
                DeliveryAddress = order.DeliveryAddress,
                OrderDate = order.OrderDate,
                CourierId = order.IdCourier,
                OrderDescription = order.OrderDescription,
                CourierReward = order.CourierReward,
                Products = order.ProductOrders.Select(product => new ProductDto {
                    ProductName = product.IdProductNavigation.ProductName,
                    ProductDescription = product.IdProductNavigation.ProductDescription,
                    ProductPrice = product.IdProductNavigation.ProductPrice
                })
            };
        }

        // POST: api/order/take
        [HttpPost]
        [Route("api/order/take")]
        public async Task<ActionResult> TakeOrder(OrderDto orderDto) {
            try {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.IdOrder == orderDto.OrderId);
                order.IdOrderStatus = 2;
                order.IdCourier = orderDto.CourierId;
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();
            }
        }

        // POST: api/order/complete
        [HttpPost]
        [Route("api/order/complete")]
        public async Task<ActionResult<bool>> CompleteOrder(OrderDto orderDto) {
            try {
                var order = await _context.Orders.FirstOrDefaultAsync(x => x.IdOrder == orderDto.OrderId);
                var courier = await _context.Couriers.FirstOrDefaultAsync(x => x.IdCourier == order.IdCourier);
                order.IdOrderStatus = 3;
                courier.CourierMoney += order.CourierReward;
                _context.Entry(order).State = EntityState.Modified;
                _context.Entry(courier).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(true);
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();
            }
        }

        // GET: api/courier/{courierId}/history-orders
        [HttpGet]
        [Route("api/couriers/{courierId}/history-orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetHistoryOrders(int courierId) {
            try {
                return Ok(ToOrderListDTO(await _context.Orders
                    .Include(x => x.IdClientNavigation)
                    .Include(x => x.IdOrderStatusNavigation)
                    .Include(x => x.ProductOrders)
                    .ThenInclude(x => x.IdProductNavigation)
                    .Where(x => x.IdOrderStatus == 3 && x.IdCourier == courierId)
                    .ToListAsync()));
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();
            }
        }

        // GET: api/courier/{courierId}/active-orders
        [HttpGet]
        [Route("api/couriers/{courierId}/active-orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetActiveOrders(int courierId) {
            try {
                return Ok(ToOrderListDTO(await _context.Orders
                    .Include(x => x.IdClientNavigation)
                    .Include(x => x.IdOrderStatusNavigation)
                    .Include(x => x.ProductOrders)
                    .ThenInclude(x => x.IdProductNavigation)
                    .Where(x => x.IdOrderStatus == 2 && x.IdCourier == courierId)
                    .ToListAsync()));
            } catch (DbUpdateConcurrencyException) {
                return BadRequest();
            }
        }
    }
}
