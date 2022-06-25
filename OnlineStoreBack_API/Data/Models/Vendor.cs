namespace OnlineStoreBack_API.Data.Models
{
	public class Vendor
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Mobile { get; set; }

		//Navegation Prop

		public virtual ICollection<Product> Products { get; set; }


	}
}
