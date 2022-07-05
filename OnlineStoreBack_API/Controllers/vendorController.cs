using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreBack_API.DTO.ModelsDTO;
using OnlineStoreBack_API.Repository;

namespace OnlineStoreBack_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class vendorController : ControllerBase
{
	private readonly IVendorRepository vendorRepository;

	public vendorController(IVendorRepository vendorRepository)
	{
		this.vendorRepository = vendorRepository;
	}
	[HttpGet]
	public ActionResult<List<VendorDTO>> Get()
	{
		var vendors = vendorRepository.GetAll();	
		List<VendorDTO> list = new List<VendorDTO>();
		foreach (var item in vendors)
		{
			list.Add(new VendorDTO { Name = item.Name, Id = item.Id });
		}
		return list;
	}







}
