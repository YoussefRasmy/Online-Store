using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class VendorMobile
	{
		[ForeignKey("Vendor")]
		public int VendorId { get; set; }
		public string Mobile { get; set; }

		public Vendor Vendor { get; set; }
	}
}