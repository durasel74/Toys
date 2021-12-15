using System;

namespace ToysClient.Model
{
	public class Client
	{
		public Client()
		{
			Sfm = "";
			PhoneNumber = "";
		}

		public long IdClient { get; set; }
		public string Sfm { get; set; }
		public string PhoneNumber { get; set; }
	}
}
