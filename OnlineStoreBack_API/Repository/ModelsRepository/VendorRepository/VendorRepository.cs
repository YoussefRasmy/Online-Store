using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Repository
{
	public class VendorRepository : GenericRepository<Vendor, int>,IVendorRepository
	{
		private readonly OnlineStoreContext db;

		public VendorRepository(OnlineStoreContext context) : base(context)
		{
			this.db = context;
		}
	}
}
