using Newtonsoft.Json;

namespace MCSM_Core.Schems
{
	public class ForgePing
	{
		[JsonProperty("version")]
		public Version Version { get; set; }

		[JsonProperty("players")]
		public PlayersPayload Players { get; set; }

		[JsonProperty("description")]
		public ForgeMotd Motd { get; set; }

		[JsonProperty("favicon")]
		public string Icon { get; set; }

		[JsonProperty("modInfo")]
		public ModInfo ModInfo { get; set; }
	}
}
