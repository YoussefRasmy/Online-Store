using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO;

public interface ICategoryToDTO
{

	//now i want to trasfir from category to categoryReadDto
	public List<CategoryReadDTO> changeToManyDTOs(List<Category> categories);

	public List<CategoryDetailsDTO> changeToCategoryDetailsDTOs(List<Category> categories);







}
