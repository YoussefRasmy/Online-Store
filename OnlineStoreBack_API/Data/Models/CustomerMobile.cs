using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class CustomerMobile
	{
		public string MobileNumber { get; set; }

		public virtual Customer Customer { get; set; }


	}
}