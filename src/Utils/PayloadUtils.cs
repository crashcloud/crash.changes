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
			if (string.IsNullOrEmpty(change?.Payload))
			{
				payload = new PayloadPacket();
				return false;
			}

			payload = new PayloadPacket();
			var parts = TryGetPayloadParts(change.Payload, out string data, out CTransform transform, out Dictionary<string, string> updates);
			if (parts != PayloadParts.All) return false;

			payload = new PayloadPacket { Data = data, Transform = transform, Updates = updates };
			return true;
		}

		/// <summary>Returns all of the parts of a payload</summary>
		/// <param name="payload">A string chunk of data, likely from a change</param>
		/// <param name="data">A string chunk of data</param>
		/// <param name="transform"></param>
		/// <param name="updates"></param>
		/// <returns></returns>
		public static PayloadParts TryGetPayloadParts(string payload, out string data, out CTransform transform, out Dictionary<string, string> updates)
		{
			data = string.Empty;
			transform = CTransform.Unset;
			updates = new();
			if (string.IsNullOrEmpty(payload)) return PayloadParts.None;

			PayloadParts result = PayloadParts.None;
			try
			{
				var jsonDocument = JsonDocument.Parse(payload);
				if (jsonDocument is null) return PayloadParts.None;

				try
				{
					if (jsonDocument.RootElement.TryGetProperty("Data", out JsonElement dataElement))
					{
						data = dataElement.GetString() ?? string.Empty;
						result |= PayloadParts.Data;
					}
				}
				catch
				{
				}

				try
				{
					if (jsonDocument.RootElement.TryGetProperty("Transform", out JsonElement transformElement))
					{
						var raw = transformElement.GetRawText();
						transform = JsonSerializer.Deserialize<CTransform>(raw);
						result |= PayloadParts.Transform;
					}
				}
				catch
				{
				}

				try
				{
					if (jsonDocument.RootElement.TryGetProperty("Updates", out JsonElement updatesElement))
					{
						// if (updatesElement.ValueKind != JsonValueKind.Array)
						var raw = updatesElement.GetRawText();
						var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(raw);

						updates = dict ?? new();
						result |= PayloadParts.Updates;
					}
				}
				catch
				{
					updates = new();
				}
			}
			catch (Exception ex)
			{

			}

			return result;
		}
	}

	[Flags]
	public enum PayloadParts
	{
		None = 0,
		Data = 1 << 1,
		Transform = 1 << 2,
		Updates = 1 << 3,
		All = Data | Transform | Updates
	}
}
