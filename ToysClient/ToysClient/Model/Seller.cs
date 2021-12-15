using System;

namespace ToysClient.Model
{
	public class Seller
	{
		public Seller()
		{
			Sfm = "";
			PhoneNumber = "";
		}

		public long IdSeller { get; set; }
		public string Sfm { get; set; }
		public string PhoneNumber { get; set; }
	}
}
