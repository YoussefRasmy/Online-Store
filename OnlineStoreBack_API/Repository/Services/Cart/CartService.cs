using Microsoft.AspNetCore.Identity;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.Repository.Services.User;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository.Services
{
	public class CartService : ICartService
	{
		private readonly IUserService userService;
		private readonly UserManager<StoreUser> userManager;
		private readonly ICartRepository cartRepository;

		public CartService(IUserService userService, UserManager<StoreUser> userManager,ICartRepository cartRepository)
		{
			this.userService = userService;
			this.userManager = userManager;
			this.cartRepository = cartRepository;
		}
		public async Task<Cart> GetUserCart(ClaimsPrincipal _user)
		{
			
			var userId = await userService.GetUserId(_user);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			return currentUserCart;
		}

		
	}
}
