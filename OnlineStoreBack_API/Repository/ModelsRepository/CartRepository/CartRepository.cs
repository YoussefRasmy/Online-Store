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

		public CartRepository(OnlineStoreContext context,UserManager<StoreUser> userManager, IHttpContextAccessor httpContextAccessor) : base(context)
		{
			this.db = context;
			this.userManager = userManager;
			this.httpContext = httpContextAccessor;
		}

		public void AddToCart(ProductCart productCart)
		{
			var cart = GetByCurrerntUserId();
			if (cart.ProductCarts.Contains(productCart))
			{
				//var cartItem = db.CartProducts.FirstOrDefault(x => x.ProductId == productCart.ProductId&&x.CartId==cart.Id);
				productCart.Quantity++;
				productCart.TotalPrice += db.Products.FirstOrDefault(x => x.Id == productCart.ProductId).Price;
			}
			else
			{
				db.CartProducts.Add(productCart);
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
				if (productCart.Quantity>1)
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
	}
}
