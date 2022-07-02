using OnlineStoreBack_API.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.DTO.ModelsDTO
{
	public class ProducReadtDTO
	{
		public int id { get; set; }
		public string EnglishName { get; set; }
		public string ImagePath { get; set; }
		public string ArabicName { get; set; }

		public CategoryReadDTO Category { get; set; }
		public VendorDTO Vendor { get; set; }

		public string Description { get; set; }


		public float Quantity { get; set; }
	
		public float Price { get; set; }
	}
}
