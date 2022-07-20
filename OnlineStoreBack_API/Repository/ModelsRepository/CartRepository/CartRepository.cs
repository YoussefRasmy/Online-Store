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
		private readonly IProductOrderRepository productOrderRepository;
		private readonly UserManager<StoreUser> userManager;
		private readonly IOrderRepository orderRepository;
		private readonly IHttpContextAccessor httpContext;
		
		private readonly IProductRepository productRepository;
		private readonly IProductCartRepository productCartRepository;

		public CartRepository(OnlineStoreContext context, IProductOrderRepository productOrderRepository,UserManager<StoreUser> userManager, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IProductCartRepository productCartRepository) : base(context)
		{
			this.db = context;
			this.productOrderRepository = productOrderRepository;
			this.userManager = userManager;
			this.orderRepository = orderRepository;
			this.httpContext = httpContextAccessor;
			
			this.productRepository = productRepository;
			this.productCartRepository = productCartRepository;
		}



		public int AddToCart(ProductCart productCart)
		{

			var cart = GetById(productCart.CartId);
			var product = productRepository.GetById(productCart.ProductId);
			if (db.CartProducts.Contains(productCart))
			{
				if (product.Quantity >= 1 && product.Quantity >= productCart.Quantity)
				{
					var TheProductCart = db.CartProducts.FirstOrDefault(x => x.ProductId == productCart.ProductId && x.CartId == productCart.CartId);
					TheProductCart.Quantity = productCart.Quantity;
					productCartRepository.CalculatePrice(TheProductCart);
				}
			}
			else
			{
				if (product.Quantity >= productCart.Quantity)
				{
					db.CartProducts.Add(productCart);
					
					//productRepository.DecreacInventorty(productCart.ProductId, productCart.Quantity);
					productCartRepository.CalculatePrice(productCart);
					
				}
				
			}
			//what if something happend with the database what should i do>>>>>>>>>>>>>>>>>>>>>>Important 
			//CalculateCart(cart);
			CalculateCart(cart);
			var x = db.SaveChanges();
			return x;

		}

		public void CalculateCart(Cart cart)
		{

			var cartProducts = db.CartProducts.Where(x => x.CartId == cart.Id).ToList();
			double price = 0;
			foreach (var cartProduct in cartProducts)
			{
				price += cartProduct.TotalPrice;
			}
			cart.TotalPrice = price;
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
				}
				else if (productCart.Quantity == 1)
				{
					db.CartProducts.Remove(productCart);

				}
				db.SaveChanges();
			}

		}

		public int TransfairToOrder(string address, Cart cart,int _paymentMethod,ref string errorMessage)
		{
			//var cart = GetByCurrerntUserId();
			var order = new Order { Address = address, UserId = cart.UserId, PaymentMethod = (PaymentMethod)_paymentMethod,  Order_Date = DateTime.Now, TotalPrice=cart.TotalPrice };

			orderRepository.Add(order);
			

			List<ProductOrder> productOrders = new List<ProductOrder>();

			var productCarts = productCartRepository.GetAllByCartId(cart.Id);
			

			foreach (var item in productCarts)
			{
				var product = productRepository.GetById(item.ProductId);
				if (product.Quantity<item.Quantity)
				{
					errorMessage = "some Item in your cart are not availaple anymore";
					return -1;
				}
				productOrders.Add(new ProductOrder { OrderId = order.Id, ProductId = item.ProductId, Quantity = item.Quantity, TotalPrice = item.TotalPrice });//+TotalPrice = item.TotalPrice
			}
			
			productOrderRepository.AddList(productOrders);
			foreach (var item in productOrders)
			{
				order.TotalPrice += item.TotalPrice;
			}

			db.SaveChanges();

			cart.TotalPrice = 0;
			return order.Id;

		}
	}
}
