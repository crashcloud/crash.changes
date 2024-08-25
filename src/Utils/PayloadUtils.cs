using System.Text;
using System.Text.Json;

using Crash.Geometry;

#pragma warning disable CS8603 // Possible null reference return.

namespace Crash.Changes.Utils
{
	/// <summary>Utilities related to <see cref="IChange.Payload" /> and <see cref="PayloadPacket" /> </summary>
	public static class PayloadUtils
	{
		/// <summary>
		///     Combines a base <see cref="PayloadPacket" /> and a new <see cref="PayloadPacket" />
		///     The newer packet takes priority, potentially overwriting old values on the basePacket.
		/// </summary>
		/// <param name="basePacket">The packet to add on top of</param>
		/// <param name="newPacket">The new packet</param>
		/// <returns>A combined <see cref="PayloadPacket" /></returns>
		public static PayloadPacket Combine(PayloadPacket basePacket, PayloadPacket newPacket)
		{
			if (basePacket is null && newPacket is null)
			{
				return new PayloadPacket();
			}

			if (basePacket is null && newPacket is not null)
			{
				return newPacket;
			}

			if (newPacket is null && basePacket is not null)
			{
				return basePacket;
			}

			string data = string.IsNullOrEmpty(basePacket.Data) ? newPacket.Data : basePacket.Data;
			CTransform newTransform = CTransform.Combine(basePacket.Transform, newPacket.Transform);
			Dictionary<string, string> updates = CombineDictionaries(basePacket.Updates, newPacket.Updates);

			return new PayloadPacket { Data = data, Transform = newTransform, Updates = updates };
		}

		private static Dictionary<string, string> CombineDictionaries(Dictionary<string, string> basePacketUpdates,
			Dictionary<string, string> newPacketUpdates)
		{
			if (basePacketUpdates is null && newPacketUpdates is not null)
			{
				return newPacketUpdates;
			}

			if (newPacketUpdates is null && basePacketUpdates is not null)
			{
				return basePacketUpdates;
			}

			Dictionary<string, string> resultantUpdates = basePacketUpdates;

			foreach (KeyValuePair<string, string> keyValuePair in newPacketUpdates)
			{
				basePacketUpdates[keyValuePair.Key] = keyValuePair.Value;
			}

			return resultantUpdates;
		}

		/// <summary>Returns Payload from a Change</summary>
		/// <returns>True on success, false on nulls, invalid change or failure</returns>
		public static bool TryGetPayloadFromChange(IChange change, out PayloadPacket payload)
		{
			payload = new PayloadPacket();
			if (string.IsNullOrEmpty(change?.Payload)) return false;

			try
			{
				var jsonDocument = JsonDocument.Parse(change.Payload);
				if (jsonDocument.RootElement.TryGetProperty("Data", out JsonElement dataElement))
				{
					payload.Data = dataElement.GetString();
				}

				if (jsonDocument.RootElement.TryGetProperty("Transform", out JsonElement transformElement))
				{
					var raw = transformElement.GetRawText();
					var transform = JsonSerializer.Deserialize<CTransform>(raw);

					payload.Transform = transform;
				}

				if (jsonDocument.RootElement.TryGetProperty("Updates", out JsonElement updatesElement))
				{
					var raw = updatesElement.GetRawText();
					var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(raw);

					payload.Updates = dict;
				}

				return true;
			}
			catch (Exception ex)
			{
				payload = new PayloadPacket();
				return false;
			}
		}
	}
}
