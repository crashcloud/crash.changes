using Crash.Geometry;

namespace Crash.Changes
{
	/// <summary>A Change Description</summary>
	public sealed record PayloadPacket
	{
		/// <summary>The Initialised Data, Geometry, BIM Objects etc.</summary>
		public string Data { get; set; } = string.Empty;

		/// <summary>The sum of all Transforms </summary>
		public CTransform Transform { get; set; } = CTransform.Unset;

		/// <summary>The latest property updates to the Payload</summary>
		public Dictionary<string, string>? Updates { get; set; } = new();
	}
}
