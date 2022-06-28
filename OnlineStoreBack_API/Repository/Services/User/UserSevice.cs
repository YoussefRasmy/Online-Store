using Microsoft.AspNetCore.Identity;
using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository.Services.User
{
	public class UserSevice
	{
		private readonly UserManager<StoreUser> userManager;

		public UserSevice(UserManager<StoreUser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task< string> GetUserId(ClaimsPrincipal _user)
		{
			

			var user = await userManager.GetUserAsync(_user);
			var userId = await userManager.GetUserIdAsync(user);

			return userId;
		}





	}
}
