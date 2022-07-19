using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.MabToDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStoreBack_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository productRepository;
		private readonly IProductToDTO productToDTO;
		private readonly ICategoryRepository categoryRepository;
		private readonly IVendorRepository vendorRepository;

		public ProductController(IProductRepository productRepository, IProductToDTO productToDTO, ICategoryRepository categoryRepository, IVendorRepository vendorRepository)
		{
			this.productRepository = productRepository;
			this.productToDTO = productToDTO;
			this.categoryRepository = categoryRepository;
			this.vendorRepository = vendorRepository;
		}

		#region Read



		[HttpGet]
		[Route("pagination/{pageNumber:int}/{pageSize:int}/{categoryId:int?}")]
		public ActionResult<ProductPagenationReadDTO> GetPagination(int pageNumber,int pageSize,int? categoryId)
		{

			var Products = productRepository.GetPagenation(pageNumber,pageSize, categoryId);
			var TotalCount = productRepository.GetProductsCount();

			var ProductsDTO = productToDTO.changeToManyDTOs(Products);

			

			return new ProductPagenationReadDTO { Count=TotalCount, Products = ProductsDTO};
		}




		// GET: api/<ProductController>
		[HttpGet]
		public ActionResult<List<ProducReadtDTO>> Get()
		{
			var Products = productRepository.GetAll();

			#region Commented
			//List<ProductDTO> _products = new List<ProductDTO>();

			//foreach (var product in Products)
			//{
			//	var category = categoryRepository.GetById(product.CategoryId);
			//	var vendor = vendorRepository.GetById(product.VendorId);

			//	_products.Add(new ProductDTO
			//	{
			//		ArabicName = product.ArabicName,
			//		Category = new CategoryReadDTO { Name = category.Name, Id = category.Id},
			//		EnglishName = product.EnglishName,
			//		ImagePath = product.ImagePath,
			//		Price = product.Price,
			//		Quantity = product.Quantity,
			//		Vendor = new VendorDTO { Id= vendor.Id, Name= vendor.Name},
			//		Description = product.Description,
			//		id = product.Id,
			//	});
			//}

			//return _products;
			#endregion

			var ProductsDTO = productToDTO.changeToManyDTOs(Products);

			ProductsDTO= ProductsDTO.OrderByDescending(x=>  x.Category.Id).ThenBy(x=>x.EnglishName).ToList();

			return ProductsDTO;


		}

		//// GET: api/<ProductController>/ByName
		[HttpGet]
		[Route("byname")]
		public ActionResult<List<ProducReadtDTO>> GetbyName(string enName)
		{
			var res = productRepository.GetAllByEnName(enName);
			return productToDTO.changeToManyDTOs(res);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public ActionResult<ProducReadtDTO> Get(int id)
		{
			var res = productRepository.GetById(id);
			return productToDTO.changeToOneDTO(res);
		}
		[HttpGet]
		[Route("ProductsByCategory")]
		public ActionResult<List<ProducReadtDTO>> GetByCategoryId(int id)
		{
			var Products = productRepository.GetProductByCategoryId(id);
			#region Commented
			//List<ProductDTO> _products = new List<ProductDTO>();
			//foreach (var product in Products)
			//{
			//	var category = categoryRepository.GetById(product.CategoryId);
			//	var vendor = vendorRepository.GetById(product.VendorId);

			//	_products.Add(new ProductDTO
			//	{
			//		ArabicName = product.ArabicName,
			//		Category = new CategoryReadDTO { Name = category.Name, Id = category.Id },
			//		EnglishName = product.EnglishName,
			//		ImagePath = product.ImagePath,
			//		Price = product.Price,
			//		Quantity = product.Quantity,
			//		Vendor = new VendorDTO { Id = vendor.Id, Name = vendor.Name },
			//		Description = product.Description,
			//		id = product.Id,
			//	});
			//}

			//return _products; 
			#endregion
			return productToDTO.changeToManyDTOs(Products);
		}
		#endregion

		#region Creat

		// POST api/<ProductController>
		[HttpPost]
		[Authorize]
		public IActionResult Post([FromBody] ProductWriteDTO _product)
		{
			if (ModelState.IsValid)
			{
				var product = productToDTO.changeToOneProduct(_product);
				productRepository.Add(product);
				return StatusCode(StatusCodes.Status201Created);
			}
			
				return BadRequest("some thing wrong with the model");
			
		}
		#endregion

		#region Update


		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		[Authorize]
		//public void Put(int id, [FromBody] Product productEdit)
		public void Edit(int id, [FromBody] ProductWriteDTO productEdit)
		{
			var product = productRepository.GetById(id);
			if (product != null)
			{
				product.Price = productEdit.Price;
				product.EnglishName = productEdit.EnglishName;
				product.ArabicName = productEdit.ArabicName;
				product.VendorId = productEdit.VendorId;
				product.CategoryId = productEdit.CategoryId;
				product.Description = productEdit.Description;
				product.ImagePath = productEdit.ImagePath;
				product.Quantity = productEdit.Quantity;
			}
			productRepository.Update(product);
		}
		#endregion

		#region Delete

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		[Authorize]
		public void Delete(int id)
		{
			var product = productRepository.GetById(id);
			if (product != null)
			{
				productRepository.Delete(id);
			}
		}
		#endregion

	}
}
