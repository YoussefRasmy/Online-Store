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
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository productRepository;
		private readonly ICategoryRepository categoryRepository;
		private readonly IVendorRepository vendorRepository;

		public ProductController(IProductRepository productRepository,ICategoryRepository categoryRepository, IVendorRepository vendorRepository )
		{
			this.productRepository = productRepository;
			this.categoryRepository = categoryRepository;
			this.vendorRepository = vendorRepository;
		}

		#region Read

		// GET: api/<ProductController>
		[HttpGet]
		public ActionResult<List<ProductDTO>> Get()
		{
			var Products = productRepository.GetAll();
			List<ProductDTO> _products = new List<ProductDTO>();
			foreach (var product in Products)
			{
				var category = categoryRepository.GetById(product.CategoryId);
				var vendor = vendorRepository.GetById(product.VendorId);

				_products.Add(new ProductDTO
				{
					ArabicName = product.ArabicName,
					Category = new CategoryReadDTO { Name = category.Name, Id = category.Id},
					EnglishName = product.EnglishName,
					ImagePath = product.ImagePath,
					Price = product.Price,
					Quantity = product.Quantity,
					Vendor = new VendorDTO { Id= vendor.Id, Name= vendor.Name},
					Description = product.Description,
				});
			}

			return _products;
		}

		//// GET: api/<ProductController>/ByName
		[HttpGet]
		[Route("byname")]
		public ActionResult<List<Product>> GetbyName(string enName)
		{
			return productRepository.GetAllSortByEnName(enName);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		public ActionResult<Product> Get(int id)
		{
			var res = productRepository.GetById(id);
			return res;
		}
		[HttpGet("{categoryId}")]
		[Route("ProductsByCategory")]
		public ActionResult<List<Product>> GetByCategoryId(int categoryId)
		{
			var res = productRepository.GetProductByCategoryId(categoryId);
			return res;
		}
		#endregion

		#region Creat

		// POST api/<ProductController>
		[HttpPost]
		public void Post([FromBody] Product product)
		{
			if (ModelState.IsValid)
			{
				productRepository.Add(product);
			}
		}
		#endregion

		#region Update


		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Product productEdit)
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
		}
		#endregion

		#region Delete

		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
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
