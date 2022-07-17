using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.MabToDTO;
using OnlineStoreBack_API.Repository.Services.User;

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderToDTO orderToDTO;
		private readonly UserManager<StoreUser> userManager;
		private readonly IOrderRepository orderRepository;
		private readonly IProductOrderRepository productOrderRepository;
		private readonly IUserService userSevice;
		private readonly IProductRepository productRepository;

		public OrderController(IOrderToDTO orderToDTO, UserManager<StoreUser> userManager, IOrderRepository orderRepository,IProductOrderRepository productOrderRepository ,IUserService userSevice, IProductRepository productRepository)
		{
			this.orderToDTO = orderToDTO;
			this.userManager = userManager;
			this.orderRepository = orderRepository;
			this.productOrderRepository = productOrderRepository;
			this.userSevice = userSevice;
			this.productRepository = productRepository;
		}
		[HttpGet]
		public async Task<ActionResult<List<OrderDTO>>> GetUserOrders()
		{
			var userId = await userSevice.GetUserId(User);
			var orders = orderRepository.GetAllByUserId(userId);

			#region Commented

			//int orderNum = 1;
			//List<OrderDTO> ordersDTO = new List<OrderDTO>();

			//foreach (var order in orders)
			//{
			//	var newOrder = new OrderDTO { DeliveryAddress = order.Address, OrderNum = orderNum++, id = order.Id ,TotalPrice = order.TotalPrice, _PaymentMethod = order.PaymentMethod };

			//	foreach (var item in order.ProductOrders)
			//	{
			//		var productId = item.ProductId;
			//		var product = productRepository.GetById(productId);

			//		newOrder.products.Add(new ProductOrderReadDTO{
			//			ImagePath = product.ImagePath,
			//			Price = product.Price,
			//			ProductName = product.EnglishName,
			//			ProductId = item.ProductId,
			//			Quantity = item.Quantity,

			//		});
			//	}
			//	ordersDTO.Add(newOrder);
			//}
			//return ordersDTO;
			#endregion

			return orderToDTO.changeManyToDTO(orders);
		}

		[HttpGet]
		[Route("one")]
		public ActionResult<OrderDTO> GetOrder(int id)
		{

			var order = orderRepository.GetById(id);

			#region Commented


			//var newOrder = new OrderDTO { DeliveryAddress = order.Address,id = order.Id, TotalPrice = order.TotalPrice, _PaymentMethod = order.PaymentMethod };

			//var productsOfTheOrder = productOrderRepository.GetAllByOrderId(id);
			//if (productsOfTheOrder is null)
			//{
			//	return BadRequest("there is no order");
			//}
			//foreach (var item in productsOfTheOrder)
			//{
			//	var productId = item.ProductId;
			//	var product = productRepository.GetById(productId);

			//	newOrder.products.Add(new ProductOrderReadDTO
			//	{
			//		ImagePath = product.ImagePath,
			//		Price = product.Price,
			//		ProductName = product.EnglishName,
			//		ProductId = item.ProductId,
			//		Quantity = item.Quantity,

			//	});
			//}
			//return newOrder;

			#endregion

			var orderDTO = orderToDTO.changeOneToDTO(order);
			if (orderDTO == null) return BadRequest("there is no order");

			return orderDTO;

		}

	}
}
