using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<StoreUser> userManager;
		private readonly OnlineStoreContext db;

		public UserController(IConfiguration configuration, UserManager<StoreUser> userManager, OnlineStoreContext context)
		{
			//to get the secret key
			this.configuration = configuration;
			this.userManager = userManager;
			this.db = context;
		}

		//Customer
		[HttpPost]
		[Route("regester")]
		public async Task<ActionResult<string>> Regester(RegisterDTO registerDTO)
		{
			if (ModelState.IsValid)
			{
				var newUser = new StoreUser
				{
					FirstName = registerDTO.FirstName,
					LastName = registerDTO.LastName,
					Email = registerDTO.Email,
					Address = registerDTO.Address,
					UserName = registerDTO.Username,
				};
				var userExest = await userManager.FindByNameAsync(newUser.UserName);
				if (userExest != null)
				{
					return BadRequest("User Alredy Exest");
				}
				var emailExest = await userManager.FindByEmailAsync(newUser.Email);

				if (emailExest != null)
				{
					return BadRequest("User Alredy Exest");
				}
				var creationRes = await userManager.CreateAsync(newUser, registerDTO.Password);

				if (!creationRes.Succeeded)
				{
					return BadRequest(creationRes.Errors);
				}


				//Claims
				var userClaims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, newUser.UserName),
					new Claim(ClaimTypes.Email, newUser.Email),
					new Claim(ClaimTypes.Role,"Customer")//Customer/Admin
				};


				var claimsRes = await userManager.AddClaimsAsync(newUser, userClaims);

				if (!claimsRes.Succeeded)
				{
					await userManager.DeleteAsync(newUser);
					return BadRequest(claimsRes.Errors);
				}

				var cart = new Cart { TotalPrice = 0, UserId = newUser.Id };
				 
				db.Carts.Add(cart);
				db.SaveChanges();

				return StatusCode(StatusCodes.Status201Created,"User Created Succesfully");
			}
			return BadRequest(ModelState.ErrorCount);

		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Wrong Cradentials");
			}
			var user = await userManager.FindByNameAsync(credentials.UserName);
			if (user == null)
			{
				return BadRequest("Wrong User Name or Password");
			}
			if (await userManager.IsLockedOutAsync(user))
			{
				return BadRequest("Pleas try again later You are Loked Out For Now");
			}
			bool isAuthenticated = await userManager.CheckPasswordAsync(user, credentials.Password);
			if (!isAuthenticated)
			{
				await userManager.AccessFailedAsync(user);
				return Unauthorized("Wrong User Name or Password");
			}

			//generate key
			//SecretKey
			var secretKey = configuration.GetValue<string>("SecretKey");
			var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
			var key = new SymmetricSecurityKey(secretKeyInBytes);
			var methodUsedInGenerateToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			//userClaims from userManger
			var exp = DateTime.Now.AddHours(1);
			var userClaims = await userManager.GetClaimsAsync(user);

			var JWT = new JwtSecurityToken(
				claims: userClaims,
				notBefore: DateTime.Now,
				expires: exp,
				signingCredentials: methodUsedInGenerateToken
				);

			var tokenHandler = new JwtSecurityTokenHandler();
			string tokenString = tokenHandler.WriteToken(JWT);

			return new TokenDTO
			{
				Token = tokenString,
				ExpiryDate = exp
			};


		}
	}
}
