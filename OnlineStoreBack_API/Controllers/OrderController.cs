using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.Services.User;

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly UserManager<StoreUser> userManager;
		private readonly OrderRepository orderRepository;
		private readonly UserService userSevice;
		private readonly ProductRepository productRepository;

		public OrderController(UserManager<StoreUser> userManager, OrderRepository orderRepository, UserService userSevice, ProductRepository productRepository)
		{
			this.userManager = userManager;
			this.orderRepository = orderRepository;
			this.userSevice = userSevice;
			this.productRepository = productRepository;
		}
		[HttpGet]
		public async Task<ActionResult<List<OrderDTO>>> GetUserOrders()
		{
			int orderNum = 1;
			var userId = await userSevice.GetUserId(User);
			var orders = orderRepository.GetAllByUserId(userId);
			List<OrderDTO> ordersDTO = new List<OrderDTO>();

			foreach (var order in orders)
			{
				var newOrder = new OrderDTO { DeliveryAddress = order.Address, OrderNum = orderNum++, TotalPrice = order.TotalPrice, _PaymentMethod = order.PaymentMethod };

				foreach (var item in order.ProductOrders)
				{
					var productId = item.ProductId;
					var product = productRepository.GetById(productId);
					newOrder.products.Add(new ProductOrderReadDTO { ImagePath = product.ImagePath, Price = product.Price, ProductName = product.EnglishName });
				}
				ordersDTO.Add(newOrder);
			}
			return ordersDTO;
		}
	}
}
