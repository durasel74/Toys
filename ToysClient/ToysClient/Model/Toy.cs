using System;

namespace ToysClient.Model
{
	public class Toy
	{
		public Toy()
		{
			Name = "";
			ReleaseDate = "";
			Info = "";
		}

		public long IdToy { get; set; }
		public long IdSklad { get; set; }
		public string Name { get; set; }
		public double Cost { get; set; }
		public string ReleaseDate { get; set; }
		public string Info { get; set; }
	}
}
