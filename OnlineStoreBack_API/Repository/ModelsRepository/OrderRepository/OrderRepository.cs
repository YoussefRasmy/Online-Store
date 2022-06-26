using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository
{
	public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
	{
		private readonly OnlineStoreContext db;

		public OrderRepository(OnlineStoreContext context, IHttpContextAccessor httpContextAccessor) : base(context)
		{
			this.db = context;
			HttpContextAccessor = httpContextAccessor;
		}

		public IHttpContextAccessor HttpContextAccessor { get; }

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
