using Newtonsoft.Json;

namespace MCSM_Core.Schems
{
	public class Version
	{
		[JsonProperty("protocol")]
		public int Protocol { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
