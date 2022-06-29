using OnlineStoreBack_API.Data.Models;
using System.Security.Claims;

namespace OnlineStoreBack_API.Repository.Services.User
{
	public interface IUserService
	{

		public  Task<string> GetUserId(ClaimsPrincipal _user);
	}
}
