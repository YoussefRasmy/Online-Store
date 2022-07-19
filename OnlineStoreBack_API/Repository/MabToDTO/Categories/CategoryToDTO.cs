using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.DTO.ModelsDTO;

namespace OnlineStoreBack_API.Repository.MabToDTO;

public class CategoryToDTO : ICategoryToDTO
{
	private readonly ICategoryRepository categoryRepository;

	public CategoryToDTO(ICategoryRepository categoryRepository)
	{
		this.categoryRepository = categoryRepository;
	}
	public List<CategoryDetailsDTO> changeToCategoryDetailsDTOs(List<Category> categories)
	{
		List<CategoryDetailsDTO> list = new List<CategoryDetailsDTO>();
		foreach (var item in categories)
		{
			if (item.Name == "Other")
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

	public List<CategoryReadDTO> changeToManyDTOs(List<Category> categories)
	{
		List<CategoryReadDTO> list = new List<CategoryReadDTO>();
		foreach (var item in categories)
		{
			list.Add(new CategoryReadDTO { Name = item.Name, Id = item.Id });
		}
		return list;
	}
}
