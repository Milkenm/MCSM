using Newtonsoft.Json;

namespace MCSM_Core.Schems
{
	public class ForgeMotd
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
