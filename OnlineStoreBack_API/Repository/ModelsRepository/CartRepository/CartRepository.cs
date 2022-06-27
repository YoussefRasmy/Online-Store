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
		private readonly UserManager<StoreUser> userManager;
		private readonly IHttpContextAccessor httpContext;
		private readonly ICartRepository cartRepository;
		private readonly IProductRepository productRepository;
		private readonly IProductCartRepository productCartRepository;

		public CartRepository(OnlineStoreContext context, UserManager<StoreUser> userManager,IOrderRepository orderRepository ,IHttpContextAccessor httpContextAccessor, ICartRepository cartRepository, IProductRepository productRepository, IProductCartRepository productCartRepository) : base(context)
		{
			this.db = context;
			this.userManager = userManager;
			OrderRepository = orderRepository;
			this.httpContext = httpContextAccessor;
			this.cartRepository = cartRepository;
			this.productRepository = productRepository;
			this.productCartRepository = productCartRepository;
		}

		public IOrderRepository OrderRepository { get; }

		public void AddToCart(ProductCart productCart)
		{

			var cart = cartRepository.GetByCurrerntUserId();
			var product = productRepository.GetById(productCart.ProductId);
			if (cart.ProductCarts.Contains(productCart))
			{
				if (product.Quantity >= 1 && product.Quantity > productCart.Quantity)
				{
					//var cartItem = db.CartProducts.FirstOrDefault(x => x.ProductId == productCart.ProductId&&x.CartId==cart.Id);
					productCart.Quantity++;
					productCart.TotalPrice += db.Products.FirstOrDefault(x => x.Id == productCart.ProductId).Price;
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

		public void TransfairToOrder(string address, DateTime deliverDate)
		{
			var cart = GetByCurrerntUserId();
			var order = new Order { Address= address, UserId=cart.UserId, OrderState= _OrderState.Pending, Order_Date=DateTime.Now, Deliver_Date= deliverDate };
			OrderRepository.Add(order);
			List<ProductOrder> productOrders = new List<ProductOrder>();
			foreach (var item in cart.ProductCarts)
			{
				productOrders.Add(new ProductOrder { OrderId=order.Id, ProductId=item.ProductId, Quantity = item.Quantity,TotalPrice = item.TotalPrice  });
			}
			order.ProductOrders = productOrders;
		}
	}
}
