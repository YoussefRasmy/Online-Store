using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.MabToDTO;
using OnlineStoreBack_API.Repository.Services;
using OnlineStoreBack_API.Repository.Services.User;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly IProductRepository productRepository;
		private readonly IProductToDTO productToDTO;
		private readonly UserManager<StoreUser> userManager;
		private readonly ICartService cartService;
		private readonly ICartRepository cartRepository;
		private readonly IUserService userSevice;
		private readonly IProductCartRepository productCartRepository;

		public CartController(IProductRepository productRepository, IProductToDTO productToDTO, UserManager<StoreUser> userManager, ICartService cartService, ICartRepository cartRepository, IUserService userSevice, IProductCartRepository productCartRepository)
		{
			this.productRepository = productRepository;
			this.productToDTO = productToDTO;
			this.userManager = userManager;
			this.cartService = cartService;
			this.cartRepository = cartRepository;
			this.userSevice = userSevice;
			this.productCartRepository = productCartRepository;
		}

		#region Read

		// GET: api/<CartController>
		[HttpGet]
		public async Task<ActionResult<cartReadDTO>> Get()
		{
			var user = await userManager.GetUserAsync(User);
			var userId = await userManager.GetUserIdAsync(user);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			List<ProductCartDTOOutput> products = new List<ProductCartDTOOutput>();

			var cartProducts = productCartRepository.GetAllByCartId(currentUserCart.Id);


			foreach (var item in cartProducts)
			{
				products.Add(new ProductCartDTOOutput
				{
					Quantity = item.Quantity,
					_Product = productToDTO.changeToOneDTO(productRepository.GetById(item.ProductId))
				});
			}


			//var cartProducts
			var cartReadDTO = new cartReadDTO { Id = currentUserCart.Id, UserId = userId, Products = products };


			return cartReadDTO;
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
		public async Task<IActionResult> AddToCart([FromBody] ProductCartDTOInput productCart)//.....,string userid......>>>>>>>> somthing is wrong
		{
			//var userId = await userSevice.GetUserId(User);0006da07-2451-4a69-94f2-2acce36a8278
			var currentUserCart = cartRepository.GetBytUserId("0006da07-2451-4a69-94f2-2acce36a8278");
			var newProductCart = new ProductCart { CartId = currentUserCart.Id, ProductId = productCart.ProductId, Quantity = productCart.Quantity };
			var res = cartRepository.AddToCart(newProductCart);//// first time it givs me 2 then the second 1 then 0 so what is going on ??
			if (res == 0)/////////////////////////////////////////>>>>>>>>>>>>>>>>>>>>> 
			{
				return BadRequest("Something went Wrong ");
			}
			return Ok(res);
		}

		#endregion

		#region add List

		// PUT api/<CartController>/5
		[HttpPut]
		public IActionResult Put(List<ProductCartDTOInput> productCartDTOs)
		{


			//var userId = await userSevice.GetUserId(User);0006da07-2451-4a69-94f2-2acce36a8278
			var currentUserCart = cartRepository.GetBytUserId("0006da07-2451-4a69-94f2-2acce36a8278");

			productCartRepository.ClearCart(currentUserCart.Id);

			productCartDTOs.ForEach(x =>
				{

					productCartRepository.AddProductCart(new ProductCart { CartId = currentUserCart.Id, ProductId = x.ProductId, Quantity = x.Quantity });

				}
				);
			return Ok();


		}

		#endregion

		#region TransfairToOrder

		[HttpPost]
		// POST api/<CartController>
		[Route("cartToOrder")]
		public async void TransfairToOrder([FromBody] OrderInfoDTO orderInfoDTO)//.......string userId....>>>>>>>> somthing is wrong
		{
			//var user = await userManager.GetUserAsync(User);
			//var userId = await userManager.GetUserIdAsync(user);

			var userId = await userSevice.GetUserId(User);
			var currentUserCart = cartRepository.GetBytUserId(userId);
			cartRepository.TransfairToOrder(orderInfoDTO.DeliveryAddress, orderInfoDTO.DeliverDate, currentUserCart);
			productCartRepository.ClearCart(currentUserCart.Id);

		}

		#endregion


		#region DeleteOne
		//Delete one product from the cart
		// GET: api/<CartController>
		[HttpDelete]
		[Route("DeleteOne")]
		public IActionResult DeleteOne(int productId)
		{
			//var user = await userManager.GetUserAsync(User);
			//var userId = await userManager.GetUserIdAsync(user);
			//var currentUserCart = cartRepository.GetBytUserId(userId);
			var currentUserCart = cartService.GetUserCart(User);
			productCartRepository.Delete(currentUserCart.Id, productId);
			return Ok();
		}

		#endregion

		#region DeleteAll
		//Delete All product from the cart
		[HttpDelete]
		[Route("DeleteAll")]
		public IActionResult DeleteAll(int productId)
		{
			//var user = await userManager.GetUserAsync(User);
			//var userId = await userManager.GetUserIdAsync(user);
			//var currentUserCart = cartRepository.GetBytUserId(userId);
			var currentUserCart = cartService.GetUserCart(User);
			productCartRepository.ClearCart(currentUserCart.Id);
			return Ok();
		}
		#endregion

	}
}
