using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository
{
	public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
	{
		private readonly OnlineStoreContext db;
		private readonly UserManager<StoreUser> userManager;
		private readonly IProductOrderRepository productOrderRepository;

		public OrderRepository(OnlineStoreContext context, IHttpContextAccessor httpContextAccessor,UserManager<StoreUser> userManager , ICartRepository cartRepository,IProductOrderRepository productOrderRepository) : base(context)
		{
			this.db = context;
			HttpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
			CartRepository = cartRepository;
			this.productOrderRepository = productOrderRepository;
		}

		public IHttpContextAccessor HttpContextAccessor { get; }
		public ICartRepository CartRepository { get; }

		public async void AddProducts()
		{
			var UserId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			//var user = await userManager.FindByIdAsync(UserId);
			var cart = CartRepository.GetBytUserId(UserId);
			
			throw new NotImplementedException();
		}

		public List<Order> GetAllByCurrentUserId()
		{
			var UserId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var orders = db.Orders.Include(x=>x.ProductOrders).Where(x=>x.UserId==UserId).ToList();
			return orders;
		}

		public List<Order> GetAllByUserId(string userId)
		{
			var orders = db.Orders.Include(x => x.ProductOrders).Where(x => x.UserId.Equals(userId) ).ToList();
			return orders;
		}
	}
}
