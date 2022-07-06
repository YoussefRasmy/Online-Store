﻿using Microsoft.AspNetCore.Mvc;
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
		public ActionResult<List<CategoryReadDTO>> Get()
		{
			var categores = categoryRepository.GetSupAndParentOnly();
			List<CategoryReadDTO> list = new List<CategoryReadDTO>();
			foreach (var item in categores)
			{
				list.Add(new CategoryReadDTO { Name = item.Name, Id = item.Id });
			}
			return list;
		}

		[HttpGet]
		[Route("all")]
		public ActionResult<List<CategoryDetailsDTO>> GetAll()
		{
			var categores = categoryRepository.GetAll();
			List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			foreach (var item in categores)
			{
				if (item.Id == 7)
				{
					continue;
				}
				var parent = categoryRepository.GetById(item.ParentCategoryId);
				if (parent == null)
				{
					list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id});
				}
				else
				{
					list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parent.Name, ParentCategoryId= parent.Id });
				}
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
		public ActionResult<List<CategoryDetailsDTO>> GetByName(string name)
		{
			var categores = categoryRepository.GetByName(name);
			List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
			foreach (var item in categores)
			{
				if (item.Id==7)
				{
					continue;
				}
				var parent = categoryRepository.GetById(item.ParentCategoryId);
				if (parent == null)
				{
					list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id });
				}
				else
				{
					list.Add(new CategoryDetailsDTO { Name = item.Name, Id = item.Id, ParentCategoryName = parent.Name, ParentCategoryId = parent.Id });
				}
			}
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
		public void Delete(int id)
		{
			categoryRepository.Delete(id);
		}
	}
}
