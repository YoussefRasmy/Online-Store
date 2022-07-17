using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO;

public interface IOrderToDTO
{

	public List<OrderDTO> changeManyToDTO(List<Order> orders);
	public OrderDTO changeOneToDTO(Order order);



}
