using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository
{
	public class CartRepository : GenericRepository<Cart, int>, ICartRepository
	{
		private readonly OnlineStoreContext db;
		private readonly ProductOrderRepository productOrderRepository;
		private readonly UserManager<StoreUser> userManager;
		private readonly IOrderRepository orderRepository;
		private readonly IHttpContextAccessor httpContext;
		private readonly ICartRepository cartRepository;
		private readonly IProductRepository productRepository;
		private readonly IProductCartRepository productCartRepository;

		public CartRepository(OnlineStoreContext context, ProductOrderRepository productOrderRepository,UserManager<StoreUser> userManager, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, IProductRepository productRepository, IProductCartRepository productCartRepository) : base(context)
		{
			this.db = context;
			this.productOrderRepository = productOrderRepository;
			this.userManager = userManager;
			this.orderRepository = orderRepository;
			this.httpContext = httpContextAccessor;
			this.cartRepository = cartRepository;
			this.productRepository = productRepository;
			this.productCartRepository = productCartRepository;
		}



		public void AddToCart(ProductCart productCart)
		{

			var cart = cartRepository.GetById(productCart.CartId);
			var product = productRepository.GetById(productCart.ProductId);
			if (cart.ProductCarts.Contains(productCart))
			{
				if (product.Quantity >= 1 && product.Quantity >= productCart.Quantity)
				{
					//productCart.Quantity++;
					//productCart.TotalPrice += db.Products.FirstOrDefault(x => x.Id == productCart.ProductId).Price;
					var TheProductCart = db.CartProducts.FirstOrDefault(x => x.ProductId == productCart.ProductId && x.CartId == productCart.CartId);
					TheProductCart.Quantity = productCart.Quantity;

				}
			}
			else
			{
				if (product.Quantity >= 1)
				{
					db.CartProducts.Add(productCart);
				}
			}
			//what if something happend with the database what should i do>>>>>>>>>>>>>>>>>>>>>>Important 
			db.SaveChanges();

		}

		public Cart GetByCurrerntUserId()
		{
			var UserId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var cart = GetBytUserId(UserId);
			return cart;

		}

		public Cart GetBytUserId(string userId)
		{

			var cart = db.Carts.Include(x => x.ProductCarts).FirstOrDefault(x => x.UserId == userId);
			if (cart.ProductCarts != null)
			{
				cart.TotalPrice = 0;
				foreach (var item in cart.ProductCarts)
				{
					cart.TotalPrice += item.TotalPrice;
				}
			}
			return cart;
		}

		public void suptrackFromCart(ProductCart productCart)
		{
			var cart = GetByCurrerntUserId();
			if (cart.ProductCarts.Contains(productCart))
			{
				if (productCart.Quantity > 1)
				{
					productCart.Quantity--;
					productCart.TotalPrice -= db.Products.FirstOrDefault(x => x.Id == productCart.ProductId).Price;
				}
				else if (productCart.Quantity == 1)
				{
					db.CartProducts.Remove(productCart);

				}
				db.SaveChanges();
			}

		}

		public void TransfairToOrder(string address, DateTime deliverDate, Cart cart)
		{
			//var cart = GetByCurrerntUserId();
			var order = new Order { Address = address, UserId = cart.UserId, OrderState = _OrderState.Pending, Order_Date = DateTime.Now, Deliver_Date = deliverDate };

			orderRepository.Add(order);

			List<ProductOrder> productOrders = new List<ProductOrder>();
			foreach (var item in cart.ProductCarts)
			{
				productOrders.Add(new ProductOrder { OrderId = order.Id, ProductId = item.ProductId, Quantity = item.Quantity, TotalPrice = item.TotalPrice });
				///---------------------------------------

			}
			//order.ProductOrders = productOrders;
			productOrderRepository.AddList(productOrders);// to let the inventory know
			order.TotalPrice = 0;
			foreach (var item in productOrders)
			{
				order.TotalPrice += item.TotalPrice;
			}
		}
	}
}
