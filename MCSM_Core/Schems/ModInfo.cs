using Newtonsoft.Json;

namespace MCSM_Core.Schems
{
	public class ModInfo
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("modList")]
		public string[] ModList { get; set; }
	}
}
