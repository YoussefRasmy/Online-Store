using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.Services.User;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly UserManager<StoreUser> userManager;
		private readonly CartRepository cartRepository;
		private readonly UserSevice userSevice;

		public CartController(UserManager<StoreUser> userManager,CartRepository cartRepository,UserSevice userSevice)
		{
			this.userManager = userManager;
			this.cartRepository = cartRepository;
			this.userSevice = userSevice;
		} 

		#region Read

		// GET: api/<CartController>
		[HttpGet]
		public async Task< ActionResult<Cart> > Get()
		{
			var user = await userManager.GetUserAsync(User);
			var userId = await userManager.GetUserIdAsync(user);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			return currentUserCart;
		}

		// GET api/<CartController>/5
		//[HttpGet("{id}")]
		//public ActionResult<Cart> Get(int id)
		//{
		//	return cartRepository.GetById(id);
		//}

		#endregion

		#region Add One

		// POST api/<CartController>
		[HttpPost]
		public async void AddToCart([FromBody] ProductCartDTO productCart)//...........>>>>>>>> somthing is wrong
		{
			var userId = await userSevice.GetUserId(User);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			var newProductCart = new ProductCart { CartId=currentUserCart.Id, ProductId=productCart.ProductId, Quantity = productCart.Quantity};
			cartRepository.AddToCart(newProductCart);
		}

		#endregion

		#region add List

		// PUT api/<CartController>/5
		[HttpPut]
		public async void Put(List<ProductCartDTO> productCartDTOs)
		{
			var userId = await userSevice.GetUserId(User);
			var currentUserCart = cartRepository.GetBytUserId(userId);

			currentUserCart.ProductCarts.Clear();

			productCartDTOs.ForEach(x =>
			currentUserCart.ProductCarts.Add(new ProductCart { CartId=currentUserCart.Id, ProductId=x.ProductId, Quantity = x.Quantity})
			);

		}

		#endregion

		#region TransfairToOrder

		[HttpPost]
		// POST api/<CartController>
		[Route("cartToOrder")]
		public async void TransfairToOrder([FromBody] string address, DateTime deliverDate)//...........>>>>>>>> somthing is wrong
		{
			//var user = await userManager.GetUserAsync(User);
			//var userId = await userManager.GetUserIdAsync(user);

			var userId = await userSevice.GetUserId(User);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			 cartRepository.TransfairToOrder(address, deliverDate, currentUserCart);

		}

		#endregion

	}
}
