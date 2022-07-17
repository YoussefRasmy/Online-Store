using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO
{
	public interface IProductToDTO
	{
		ProducReadtDTO changeToOneDTO(Product product);
		List<ProducReadtDTO> changeToManyDTOs(List<Product> _products);


		Product changeToOneProduct(ProductWriteDTO product);
		List<Product> changeToManyProducts(List<ProductWriteDTO> _products);
	}
}
