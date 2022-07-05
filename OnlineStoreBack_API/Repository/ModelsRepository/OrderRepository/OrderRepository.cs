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


		public OrderRepository(OnlineStoreContext context) : base(context)
		{
			this.db = context;

		}

		#region commented

		//public async void AddProducts()
		//{
		//	var UserId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		//	//var user = await userManager.FindByIdAsync(UserId);
		//	var cart = CartRepository.GetBytUserId(UserId);
		// >>>>>>>>>>>>>>>>>>>>>>>>>Done in the cart repo
		//	throw new NotImplementedException();
		//}

		//public List<Order> GetAllByCurrentUserId()
		//{
		//	var UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		//	var orders = db.Orders.Include(x=>x.ProductOrders).Where(x=>x.UserId==UserId).ToList();
		//	return orders;
		//} 
		#endregion

		public List<Order> GetAllByUserId(string userId)
		{
			var orders = db.Orders.Include(x => x.ProductOrders).Where(x => x.UserId.Equals(userId) ).ToList();
			return orders;
		}
	}
}
