using System;

namespace ToysClient.Model
{
	public class Sklad
	{
		public Sklad()
		{
			Address = "";
		}

		public long IdSklad { get; set; }
		public string Address { get; set; }
	}
}
