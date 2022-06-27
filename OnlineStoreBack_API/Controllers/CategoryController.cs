using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly OnlineStoreContext db;
		private readonly CategoryRepository categoryRepository;
		private readonly IHttpContextAccessor httpContextAccessor;

		public CategoryController(OnlineStoreContext context, CategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
		{
			this.db = context;
			this.categoryRepository = categoryRepository;
			this.httpContextAccessor = httpContextAccessor;
		}



		#region Read
		// GET: api/<CategoryController>
		[HttpGet]
		public ActionResult<List<Category>> Get()
		{
			return categoryRepository.GetAll();
		}

		// GET api/<CategoryController>/5

		[HttpGet("{id}")]
		public ActionResult<Category> Get(int id)
		{
			return categoryRepository.GetById(id);
		}

		[HttpGet("{name:string}")]

		#endregion

		// POST api/<CategoryController>
		[HttpPost]
		public void Post([FromBody] Category category)
		{
			categoryRepository.Add(category);

		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Category category)
		{
			categoryRepository.Update(category);
		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			categoryRepository.Delete(id);
		}
	}
}
