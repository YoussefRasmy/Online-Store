using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO;

public class OrderToDTO : IOrderToDTO
{
	private readonly IProductRepository productRepository;
	private readonly IProductOrderRepository productOrderRepository;

	public OrderToDTO(IProductRepository productRepository,IProductOrderRepository productOrderRepository)
	{
		this.productRepository = productRepository;
		this.productOrderRepository = productOrderRepository;
	}
	public List<OrderDTO> changeManyToDTO(List<Order> orders)
	{
		int orderNum = 1;
		List<OrderDTO> ordersDTO = new List<OrderDTO>();

		foreach (var order in orders)
		{
			var newOrder = new OrderDTO { DeliveryAddress = order.Address, OrderNum = orderNum++, id = order.Id, TotalPrice = order.TotalPrice, _PaymentMethod = order.PaymentMethod };

			foreach (var item in order.ProductOrders)
			{
				var productId = item.ProductId;
				var product = productRepository.GetById(productId);

				newOrder.products.Add(new ProductOrderReadDTO
				{
					ImagePath = product.ImagePath,
					Price = product.Price,
					ProductName = product.EnglishName,
					ProductId = item.ProductId,
					Quantity = item.Quantity,

				});
			}
			ordersDTO.Add(newOrder);
		}
		return ordersDTO;
	}

	public OrderDTO changeOneToDTO(Order order)
	{
		var newOrder = new OrderDTO { DeliveryAddress = order.Address, id = order.Id, TotalPrice = order.TotalPrice, _PaymentMethod = order.PaymentMethod };

		var productsOfTheOrder = productOrderRepository.GetAllByOrderId(order.Id);
		if (productsOfTheOrder is null)
		{
			return null;
		}
		foreach (var item in productsOfTheOrder)
		{
			var productId = item.ProductId;
			var product = productRepository.GetById(productId);

			newOrder.products.Add(new ProductOrderReadDTO
			{
				ImagePath = product.ImagePath,
				Price = product.Price,
				ProductName = product.EnglishName,
				ProductId = item.ProductId,
				Quantity = item.Quantity,

			});
		}
		return newOrder;
	}
}
