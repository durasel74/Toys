using System;

namespace ToysServer.Model
{
	/// <summary>
	/// Журнал из базы данных.
	/// </summary>
	public class Journal
	{
		public long Id { get; set; }
		public long IdToy { get; set; }
		public long IdClient { get; set; }
		public long IdSeller { get; set; }
		public long Count { get; set; }
		public string Date { get; set; }
	}
}
