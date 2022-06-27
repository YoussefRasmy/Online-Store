using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly OnlineStoreContext db;
		private readonly CartRepository cartRepository;

		public CartController(OnlineStoreContext context,CartRepository cartRepository)
		{
			this.db = context;
			this.cartRepository = cartRepository;
		}

		#region Read

		// GET: api/<CartController>
		[HttpGet]
		public ActionResult<Cart> Get()
		{
			return cartRepository.GetByCurrerntUserId();
		}

		// GET api/<CartController>/5
		[HttpGet("{id}")]
		public ActionResult<Cart> Get(int id)
		{
			return cartRepository.GetById(id);
		}

		#endregion

		#region Creat

		// POST api/<CartController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
			
		}

		#endregion

		#region Update

		// PUT api/<CartController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		#endregion

		#region Delete

		// DELETE api/<CartController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
		#endregion
	}
}
