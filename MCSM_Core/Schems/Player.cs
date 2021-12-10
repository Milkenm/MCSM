using Newtonsoft.Json;

namespace MCSM_Core.Schems
{
	public class Player
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }
	}
}
