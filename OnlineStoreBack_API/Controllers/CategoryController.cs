using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		
		private readonly ICategoryRepository categoryRepository;
		

		public CategoryController( ICategoryRepository categoryRepository)
		{
			
			this.categoryRepository = categoryRepository;
		}



		#region Read
		// GET: api/<CategoryController>
		[HttpGet]
		public ActionResult<List<CategoryDetailsDTO>> Get()
		{
			var categores = categoryRepository.GetSupAndParentOnly();
			List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			foreach (var item in categores)
			{
				var parentId = item.ParentCategoryId;
				if (parentId != null)
				{
					var parentCategory = categoryRepository.GetById((int)item.ParentCategoryId);
					list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parentCategory.Name });
				}
				
				list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id  });
			}
			return list;
		}

		// GET api/<CategoryController>/5

		[HttpGet("{id}")]
		public ActionResult<Category> Get(int id)
		{
			return categoryRepository.GetById(id);
		}
		// GET api/<CategoryController>/Name
		[HttpGet]
		[Route("Name")]
		public ActionResult<List<Category>> GetByName(string name)
		{
			return categoryRepository.GetByName(name);
		}

		#endregion

		// POST api/<CategoryController>
		[HttpPost]
		public void Post([FromBody] CategoryWriteDTO categoryDTO)
		{
			var category = new Category { Name= categoryDTO.Name, ParentCategoryId = categoryDTO?.ParentCategoryId };
			categoryRepository.Add(category);

		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Category category)
		{
			
			var cat = categoryRepository.GetById(id);	
			cat.Name = category.Name;
			cat.ParentCategoryId = category.ParentCategoryId;
		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			categoryRepository.Delete(id);
		}
	}
}
