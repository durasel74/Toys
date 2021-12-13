using System;

namespace ToysServer.Model
{
	/// <summary>
	/// Покупатель из базы данных.
	/// </summary>
	public class Client
	{
		public long IdClient { get; set; }
		public string Sfm { get; set; }
		public string PhoneNumber { get; set; }
	}
}
