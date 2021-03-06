using System;

namespace ToysServer.Model
{
	/// <summary>
	/// Игрушка из базы данных.
	/// </summary>
	public class Toy
	{
		public long IdToy { get; set; }
		public long IdSklad { get; set; }
		public string Name { get; set; }
		public double Cost { get; set; }
		public string ReleaseDate { get; set; }
		public string Info { get; set; }
	}
}
