using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.MabToDTO;

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		
		private readonly ICategoryRepository categoryRepository;
		private readonly ICategoryToDTO categoryToDTO;

		public CategoryController( ICategoryRepository categoryRepository, ICategoryToDTO categoryToDTO  )
		{
			
			this.categoryRepository = categoryRepository;
			this.categoryToDTO = categoryToDTO;
		}



		#region Read
		// GET: api/<CategoryController>
		[HttpGet]
		public ActionResult<List<CategoryReadDTO>> Get()
		{
			var categores = categoryRepository.GetSupAndParentOnly();

			#region commented

			//List<CategoryReadDTO> list = new List<CategoryReadDTO>();
			//foreach (var item in categores)
			//{
			//	list.Add(new CategoryReadDTO { Name = item.Name, Id = item.Id });
			//}
			#endregion

			var list = categoryToDTO.changeToManyDTOs(categores);

			return list;
		}

		[HttpGet]
		[Route("all")]
		public ActionResult<List<CategoryDetailsDTO>> GetAll()
		{

			var categores = categoryRepository.GetAll();

			#region commented

			//List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			//foreach (var item in categores)
			//{
			//	if (item.Id == 7)
			//	{
			//		continue;
			//	}
			//	var parent = categoryRepository.GetById(item.ParentCategoryId);
			//	if (parent == null)
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id});
			//	}
			//	else
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parent.Name, ParentCategoryId= parent.Id });
			//	}
			//}
			//return list
			#endregion

			var list = categoryToDTO.changeToCategoryDetailsDTOs(categores);

			return list;
		}

		[HttpGet]
		[Route("parents")]
		public ActionResult<List<CategoryDetailsDTO>> GetAllParent()
		{

			var categores = categoryRepository.GetParentCategoryOnly();

			#region commented

			//List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			//foreach (var item in categores)
			//{
			//	if (item.Id == 7)
			//	{
			//		continue;
			//	}
			//	var parent = categoryRepository.GetById(item.ParentCategoryId);
			//	if (parent == null)
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id});
			//	}
			//	else
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parent.Name, ParentCategoryId= parent.Id });
			//	}
			//}
			//return list
			#endregion

			var list = categoryToDTO.changeToCategoryDetailsDTOs(categores);

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
		public ActionResult<List<CategoryDetailsDTO>> GetByName(string name)
		{
			var categores = categoryRepository.GetByName(name);

			#region commented

			//List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			//foreach (var item in categores)
			//{
			//	if (item.Id==7)
			//	{
			//		continue;
			//	}
			//	var parent = categoryRepository.GetById(item.ParentCategoryId);
			//	if (parent == null)
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id });
			//	}
			//	else
			//	{
			//		list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parent.Name, ParentCategoryId = parent.Id });
			//	}
			//}

			#endregion

			var list = categoryToDTO.changeToCategoryDetailsDTOs(categores);

			return list;
		}

		#endregion

		// POST api/<CategoryController>
		[HttpPost]
		public void Post([FromBody] CategoryWriteDTO categoryDTO)
		{

			var category = new Category { Name = categoryDTO.Name, ParentCategoryId = categoryDTO?.ParentCategoryId };
			categoryRepository.Add(category);

		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] CategoryWriteDTO category)
		{
			var cat = categoryRepository.GetById(id);
			cat.Name = category.Name;
			cat.ParentCategoryId = category.ParentCategoryId;
			categoryRepository.Update(cat);
		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			categoryRepository.Delete(id);
			return Ok();
		}
	}
}
