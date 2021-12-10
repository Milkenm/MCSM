using Newtonsoft.Json;

using System.Collections.Generic;

namespace MCSM_Core.Schems
{
	public class PlayersPayload
	{
		[JsonProperty("max")]
		public int Max { get; set; }

		[JsonProperty("online")]
		public int Online { get; set; }

		[JsonProperty("sample")]
		public List<Player> Sample { get; set; }
	}
}
