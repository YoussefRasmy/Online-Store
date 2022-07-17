using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO
{
	public class ProductToDTO : IProductToDTO
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IVendorRepository vendorRepository;

		public ProductToDTO(ICategoryRepository categoryRepository, IVendorRepository vendorRepository)
		{
			this.categoryRepository = categoryRepository;
			this.vendorRepository = vendorRepository;
		}

		public List<ProducReadtDTO> changeToManyDTOs(List<Product> products)
		{
			List<ProducReadtDTO> _products = new List<ProducReadtDTO>();
			foreach (var product in products)
			{
				var category = categoryRepository.GetById(product.CategoryId);
				var vendor = vendorRepository.GetById(product.VendorId);

				_products.Add(new ProducReadtDTO
				{
					ArabicName = product.ArabicName,
					Category = new CategoryReadDTO { Name = category.Name, Id = category.Id },
					EnglishName = product.EnglishName,
					ImagePath = product.ImagePath,
					Price = product.Price,
					Quantity = product.Quantity,
					Vendor = new VendorDTO { Id = vendor.Id, Name = vendor.Name },
					Description = product.Description,
					id = product.Id,
				});
			}

			return _products;
		}

		public List<Product> changeToManyProducts(List<ProductWriteDTO> _products)
		{
			List<Product> products = new List<Product>();

			foreach (var product in _products)
			{
				products.Add(new Product
				{
					ArabicName = product.ArabicName,
					CategoryId = product.CategoryId,
					Description = product.Description,
					EnglishName = product.EnglishName,
					ImagePath = product.ImagePath,
					Quantity = product.Quantity,
					VendorId = product.VendorId,
					Price = product.Price,
				});
			}
			return products;
		}

		public ProducReadtDTO changeToOneDTO(Product product)
		{
			var category = categoryRepository.GetById(product.CategoryId);
			var vendor = vendorRepository.GetById(product.VendorId);

			var productDTO = new ProducReadtDTO
			{
				ArabicName = product.ArabicName,
				Category = new CategoryReadDTO { Name = category.Name, Id = category.Id },
				EnglishName = product.EnglishName,
				ImagePath = product.ImagePath,
				Price = product.Price,
				Quantity = product.Quantity,
				Vendor = new VendorDTO { Id = vendor.Id, Name = vendor.Name },
				Description = product.Description,
				id = product.Id,
			};
			return productDTO;
		}

		public Product changeToOneProduct(ProductWriteDTO _product)
		{


			Product product = new Product
			{
				ArabicName = _product.ArabicName,
				CategoryId = _product.CategoryId,
				Description = _product.Description,
				EnglishName = _product.EnglishName,
				ImagePath = _product.ImagePath,
				Quantity = _product.Quantity,
				VendorId = _product.VendorId,
				Price = _product.Price,
			};
			return product;
		}
	}
}
