using Crash.Geometry;

namespace Crash.Changes
{

	/// <summary>A Change Description</summary>
	public sealed class PayloadPacket
	{

		/// <summary>The Initialised Data, Geometry, BIM Objects etc.</summary>
		public string Payload { get; set; }

		/// <summary>The sum of all Transforms </summary>
		public CTransform Transform { get; set; }

		/// <summary>The latest updates to the Payload</summary>
		public Dictionary<string, string> Updates { get; set; }

	}
}
