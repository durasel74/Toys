using System;

namespace ToysServer.Model
{
	/// <summary>
	/// Продавец из базы данных.
	/// </summary>
	public class Seller
	{
		public long IdSeller { get; set; }
		public string Sfm { get; set; }
		public string PhoneNumber { get; set; }
	}
}
